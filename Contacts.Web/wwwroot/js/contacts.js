document.addEventListener("DOMContentLoaded", function () {

    // ONLY LETTERS (Name, JobTitle)
    document.querySelectorAll('.js-only-letters').forEach(input => {
        input.addEventListener('input', () => {
            input.value = input.value.replace(/[^A-Za-zА-Яа-яЁё\s]/g, '');
            input.classList.toggle('is-invalid', input.value.trim() === '');
        });
    });

    // UPDATE MODAL
    const updateModal = document.getElementById('updateModal');
    if (!updateModal) return;

    updateModal.addEventListener('show.bs.modal', e => {
        const btn = e.relatedTarget;

        document.getElementById('updateId').value = btn.dataset.id;
        document.getElementById('updateName').value = btn.dataset.name;
        document.getElementById('updatePhone').value = btn.dataset.phone;
        document.getElementById('updateJob').value = btn.dataset.job;
        document.getElementById('updateBirth').value = btn.dataset.birth;

        document.getElementById('updateForm').action =
            '/Contacts/Update/' + btn.dataset.id;
    });

});