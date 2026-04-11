import { $, apiFetch, renderStatus, getQueryParam, captureActorForm } from
	'/scripts/common.js';
(async function initActorEdit() {
	const id = getQueryParam('id');
	const form = $('#actor-form');
	const statusEl = $('#status');
	// Disables form fields and do not allow editing if actor id is missing.
	if (!id) {
		renderStatus(statusEl, 'err', 'Missing ?id in URL.');
		form.querySelectorAll('input,textarea,button,select').forEach(
			el => el.disabled = true);
		return;
	}
	// Populates form with data from actor (id) fetched from the API server.
	try {
		const a = await apiFetch(`/actors/${encodeURIComponent(id)}`);
		form.firstName.value = a.firstName ?? '';
		form.lastName.value = a.lastName ?? '';
		form.rating.value = a.rating ?? '';
		form.bio.value = a.bio ?? '';
		renderStatus(statusEl, 'ok', 'Loaded actor. You can edit and save.');
	} catch (err) {
		renderStatus(statusEl, 'err', `Failed to load data: ${err.message}`);
		return;
	}
	// Executes the given function whenever the form 'submit' event is triggered.
	form.addEventListener('submit', async (ev) => {
		ev.preventDefault();
		const payload = captureActorForm(form);
		// Input validation and feedback goes here. For example:
		//
		// if(payload.rating > 10 || payload.rating < 0) {
		// renderStatus(statusEl, 'err', 'Actor rating must be between 0 and 10.');
		// return;
		// } else if (...) {
		// ...
		// }
		try {
			const updated = await apiFetch(`/actors/${encodeURIComponent(id)}`, {
				method: 'PUT',
				body: JSON.stringify(payload),
			});
			renderStatus(statusEl, 'ok',
				`Updated actor #${updated.id} "${updated.firstName} ${updated.lastName}" (Rating: ${updated.rating}).`);
		} catch (err) {
			renderStatus(statusEl, 'err', `Update failed: ${err.message}`);
		}
	});
})();