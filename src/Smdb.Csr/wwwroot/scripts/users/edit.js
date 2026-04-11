import { $, apiFetch, renderStatus, getQueryParam, captureUserForm } from
	'/scripts/common.js';
(async function initUserEdit() {
	const id = getQueryParam('id');
	const form = $('#user-form');
	const statusEl = $('#status');
	// Disables form fields and do not allow editing if user id is missing.
	if (!id) {
		renderStatus(statusEl, 'err', 'Missing ?id in URL.');
		form.querySelectorAll('input,textarea,button,select').forEach(
			el => el.disabled = true);
		return;
	}
	// Populates form with data from user (id) fetched from the API server.
	try {
		const u = await apiFetch(`/users/${encodeURIComponent(id)}`);
		form.name.value = u.name ?? '';
		form.email.value = u.email ?? '';
		renderStatus(statusEl, 'ok', 'Loaded user. You can edit and save.');
	} catch (err) {
		renderStatus(statusEl, 'err', `Failed to load data: ${err.message}`);
		return;
	}
	// Executes the given function whenever the form 'submit' event is triggered.
	form.addEventListener('submit', async (ev) => {
		ev.preventDefault();
		const payload = captureUserForm(form);
		// Input validation and feedback goes here. For example:
		//
		// if(payload.email.length > 256) {
		// renderStatus(statusEl, 'err', 'Email should not be longer than 256 characters.');
		// return;
		// } else if (...) {
		// ...
		// }
		try {
			const updated = await apiFetch(`/users/${encodeURIComponent(id)}`, {
				method: 'PUT',
				body: JSON.stringify(payload),
			});
			renderStatus(statusEl, 'ok',
				`Updated user #${updated.id} "${updated.name}" (${updated.email}).`);
		} catch (err) {
			renderStatus(statusEl, 'err', `Update failed: ${err.message}`);
		}
	});
})();