# syntax=docker/dockerfile:1
# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
# All-in-one TESTING image: the ASP.NET app + a MySQL server in the same container.
# The database lives inside the container, so it starts empty on every run
# (EF Core migrations are applied automatically on startup by Program.cs).
#
#   docker build -f Application/Dockerfile -t gamerskeep .      (run from the solution root)
#   docker run --rm -p 8080:8080 -p 3306:3306 gamerskeep
#
# Override the DB credentials with -e MYSQL_DATABASE=... -e MYSQL_USER=... -e MYSQL_PASSWORD=...
# (if MYSQL_PASSWORD isn't given, a random one is generated and printed in the logs).

# This stage is used when running from VS in fast mode (Default for Debug configuration)
# Pinned to the Ubuntu 24.04 variant because Ubuntu (unlike Debian) ships MySQL Server packages.
FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble AS base
USER root

# "core" packages = just the server/client binaries, without the init scripts,
# systemd units and config files the full mysql-server package drags in.
RUN apt-get update \
    && apt-get install -y --no-install-recommends mysql-server-core-8.0 mysql-client-core-8.0 \
    && rm -rf /var/lib/apt/lists/* \
    && mkdir -p /var/lib/mysql /var/run/mysqld \
    && chown -R "$APP_UID:$APP_UID" /var/lib/mysql /var/run/mysqld

# Startup script: initialise + start MySQL, create the app database/user,
# then run the app. If either process exits, the whole container stops.
COPY <<'EOF' /usr/local/bin/start.sh
#!/bin/bash
set -euo pipefail

DATADIR=/var/lib/mysql
SOCKET=/var/run/mysqld/mysqld.sock

MYSQL_DATABASE="${MYSQL_DATABASE:-gamerskeep}"
MYSQL_USER="${MYSQL_USER:-gamerskeep}"

# If there's already a saved password in the persistent volume, use it.
if [ -f "$DATADIR/.gamerskeep_password" ]; then
    MYSQL_PASSWORD=$(cat "$DATADIR/.gamerskeep_password")
elif [ -z "${MYSQL_PASSWORD:-}" ]; then
    MYSQL_PASSWORD="$(head -c 18 /dev/urandom | base64 | tr -dc 'A-Za-z0-9')"
    echo "[start] Generated MySQL password for '$MYSQL_USER': $MYSQL_PASSWORD"
fi

NEEDS_INIT=0
if [ ! -d "$DATADIR/mysql" ]; then
    NEEDS_INIT=1
    echo "[start] Initialising MySQL data directory..."
    mysqld --no-defaults --initialize-insecure --datadir="$DATADIR"
fi

echo "[start] Starting MySQL..."
# Trimmed-down settings to keep the memory footprint small.
mysqld --no-defaults \
    --datadir="$DATADIR" \
    --socket="$SOCKET" \
    --pid-file=/var/run/mysqld/mysqld.pid \
    --bind-address=0.0.0.0 --port=3306 \
    --mysqlx=OFF \
    --secure-file-priv=NULL \
    --skip-name-resolve \
    --performance-schema=OFF \
    --innodb-buffer-pool-size=64M \
    --innodb-log-buffer-size=8M \
    --innodb-redo-log-capacity=64M \
    --max-connections=100 \
    --key-buffer-size=8M \
    --tmp-table-size=16M \
    --max-heap-table-size=16M \
    --character-set-server=utf8mb4 \
    --collation-server=utf8mb4_0900_ai_ci &
MYSQL_PID=$!

for _ in $(seq 60); do
    if mysql --socket="$SOCKET" -uroot -e 'SELECT 1' >/dev/null 2>&1; then break; fi
    if ! kill -0 "$MYSQL_PID" 2>/dev/null; then echo "[start] MySQL failed to start." >&2; exit 1; fi
    sleep 1
done

if [ "$NEEDS_INIT" -eq 1 ]; then
    echo "[start] Creating database '$MYSQL_DATABASE' and user '$MYSQL_USER'..."
    mysql --socket="$SOCKET" -uroot <<SQL
CREATE DATABASE IF NOT EXISTS \`$MYSQL_DATABASE\` CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
CREATE USER IF NOT EXISTS '$MYSQL_USER'@'%' IDENTIFIED BY '$MYSQL_PASSWORD';
GRANT ALL PRIVILEGES ON \`$MYSQL_DATABASE\`.* TO '$MYSQL_USER'@'%';
FLUSH PRIVILEGES;
SQL
    # Save the password for the next time the container starts with this volume
    echo "$MYSQL_PASSWORD" > "$DATADIR/.gamerskeep_password"
fi

# Environment variables take precedence over appsettings / user secrets.
export ConnectionStrings__DbDevConnection="server=127.0.0.1;port=3306;database=$MYSQL_DATABASE;user=$MYSQL_USER;password=$MYSQL_PASSWORD"

echo "[start] Starting application..."
dotnet /app/Application.dll &
APP_PID=$!

# Forward docker stop (SIGTERM) / Ctrl+C to both processes for a clean shutdown.
trap 'kill -TERM "$APP_PID" "$MYSQL_PID" 2>/dev/null' TERM INT

set +e
wait -n "$APP_PID" "$MYSQL_PID"
EXIT_CODE=$?
kill -TERM "$APP_PID" "$MYSQL_PID" 2>/dev/null
wait
exit "$EXIT_CODE"
EOF

# Strip Windows line endings in case this file gets saved with CRLF.
RUN sed -i 's/\r$//' /usr/local/bin/start.sh && chmod 755 /usr/local/bin/start.sh

# DB credentials: MYSQL_DATABASE / MYSQL_USER default to "gamerskeep" and
# MYSQL_PASSWORD is randomly generated per run unless passed with -e.

# Drop root: everything (mysqld + dotnet) runs as the unprivileged, no-login
# "app" user (UID 1654) that ships with the .NET base images.
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
# MySQL, only needs publishing if you want to inspect the DB from the host.
EXPOSE 3306
# Expose the data directory as a volume for persistence.
VOLUME ["/var/lib/mysql"]


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Application/Application.csproj", "Application/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "./Application/Application.csproj"
COPY . .
WORKDIR "/src/Application"
RUN dotnet build "./Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["/usr/local/bin/start.sh"]