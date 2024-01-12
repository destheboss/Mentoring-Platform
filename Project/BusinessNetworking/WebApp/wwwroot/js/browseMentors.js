function filterMentors() {
    var input, filter, mentorList, cards, i;
    input = document.getElementById('mentorSearch');
    filter = input.value.toUpperCase();
    mentorList = document.getElementById('mentorList');
    cards = mentorList.getElementsByClassName('mentor-card');

    for (i = 0; i < cards.length; i++) {
        var specialties = cards[i].getAttribute('data-specialties').toUpperCase();
        if (specialties.includes(filter)) {
            cards[i].style.display = "";
        } else {
            cards[i].style.display = "none";
        }
    }
}