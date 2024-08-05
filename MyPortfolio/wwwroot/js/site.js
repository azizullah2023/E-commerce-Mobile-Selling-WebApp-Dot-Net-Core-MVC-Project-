
document.addEventListener('DOMContentLoaded', function () {
    console.log('DOM fully loaded and parsed');

    var toggleButtons = document.querySelectorAll('.toggle-comments-btn');
    console.log('Buttons elements:', toggleButtons);

    toggleButtons.forEach(function (button) {
        button.addEventListener('click', function (event) {
            event.preventDefault();  // Prevent the default button behavior
            console.log('Button clicked');

            var targetId = this.getAttribute('data-target');
            var commentsSection = document.querySelector(targetId);
            console.log('Comments section:', commentsSection);

            if (commentsSection.style.display === 'none' || commentsSection.style.display === '') {
                commentsSection.style.display = 'block';
                this.textContent = 'Hide Comments';
            } else {
                commentsSection.style.display = 'none';
                this.textContent = 'Show Comments';
            }
        });
    });
});

