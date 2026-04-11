import { $, apiFetch, renderStatus, captureActorForm } from '/scripts/common.js';
(async function initActorAdd() {
	const form = $('#actor-form');
	const statusEl = $('#status');
	renderStatus(statusEl, 'ok', 'New actor. You can edit and save.');
	form.addEventListener('submit', async (ev) => {
		ev.preventDefault();
		const payload = captureActorForm(form);
		// Input validation and feedback goes here. For example:
		//
		// if(payload.firstName.length > 256) {
		// renderStatus(statusEl, 'err',
		// 'First name should not be longer than 256 characters.');
		// return;
		// } else if (...) {
		// ...
		// }
		try {
			const created = await apiFetch(
				'/actors', { method: 'POST', body: JSON.stringify(payload) });
			renderStatus(statusEl, 'ok',
				`Created actor #${created.id} "${created.firstName} ${created.lastName}" (Rating: ${created.rating}).`);
			form.reset();
		} catch (err) {
			renderStatus(statusEl, 'err', `Create failed: ${err.message}`);
		}
	});
})();