(() => {
  'use strict';

  const root = document.documentElement;
  const currentTheme = () =>
    root.getAttribute('data-theme') ||
    (matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');

  document.querySelectorAll('[data-theme-toggle]').forEach(btn => {
    btn.addEventListener('click', () => {
      const next = currentTheme() === 'dark' ? 'light' : 'dark';
      root.setAttribute('data-theme', next);
      try { localStorage.setItem('theme', next); } catch { }
    });
  });

  const navBtn = document.querySelector('[data-nav-toggle]');
  const nav = document.getElementById('site-nav');
  if (navBtn && nav) {
    const setOpen = open => {
      nav.classList.toggle('is-open', open);
      navBtn.setAttribute('aria-expanded', String(open));
      navBtn.setAttribute('aria-label', open ? 'Close menu' : 'Open menu');
    };
    navBtn.addEventListener('click', () => setOpen(!nav.classList.contains('is-open')));
    document.addEventListener('keydown', e => { if (e.key === 'Escape') setOpen(false); });
  }

  document.querySelectorAll('[data-toggle-password]').forEach(btn => {
    btn.addEventListener('click', () => {
      const input = btn.parentElement.querySelector('input');
      const show = input.type === 'password';
      input.type = show ? 'text' : 'password';
      btn.textContent = show ? 'Hide' : 'Show';
      btn.setAttribute('aria-label', show ? 'Hide password' : 'Show password');
    });
  });

  document.querySelectorAll('form[data-submit-lock]').forEach(form => {
    form.addEventListener('submit', () => {
      setTimeout(() => {
        if (window.jQuery && window.jQuery(form).valid && !window.jQuery(form).valid()) return;
        const btn = form.querySelector('button[type="submit"]');
        if (btn && !btn.disabled) {
          btn.disabled = true;
          btn.dataset.originalText = btn.textContent;
          btn.textContent = btn.dataset.loadingText || 'Please wait…';
        }
      }, 0);
    });
  });
})();