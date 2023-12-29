function toggleSpecialtiesDropdown() {
    var roleSelect = document.getElementById('roleSelect');
    var specialtiesSelect = document.getElementById('specialtiesSelect');
    var addButton = document.getElementById('addSpecialtyButton');

    var isMentor = roleSelect.value === 'Mentor';
    specialtiesSelect.disabled = !isMentor;
    addButton.disabled = !isMentor;
}

function addSpecialty() {
    var roleSelect = document.getElementById('roleSelect');
    if (roleSelect.value !== 'Mentor') {
        return;
    }

    var specialtiesSelect = document.getElementById('specialtiesSelect');
    var selectedSpecialty = specialtiesSelect.value;
    var list = document.getElementById('selectedSpecialtiesList');

    var existingItems = list.getElementsByTagName('li');
    for (var i = 0; i < existingItems.length; i++) {
        if (existingItems[i].textContent.replace('X', '').trim() === selectedSpecialty) {
            return;
        }
    }

    var listItem = document.createElement('li');
    listItem.textContent = selectedSpecialty;

    var deleteButton = document.createElement('button');
    deleteButton.textContent = 'X';
    deleteButton.onclick = function () {
        list.removeChild(listItem);
        updateHiddenInput();
    };

    listItem.appendChild(deleteButton);
    list.appendChild(listItem);

    updateHiddenInput();
}

function updateHiddenInput() {
    var roleSelect = document.getElementById('roleSelect');
    var hiddenInput = document.getElementById('specialtiesHidden');

    if (roleSelect.value === 'Mentor') {
        var list = document.getElementById('selectedSpecialtiesList');
        var allListItems = list.getElementsByTagName('li');
        var specialties = Array.from(allListItems).map(function (item) {
            return item.firstChild.textContent;
        });

        hiddenInput.value = specialties.join(',');
    } else {
        hiddenInput.value = '';
    }
}

document.addEventListener('DOMContentLoaded', function () {
    toggleSpecialtiesDropdown();
});