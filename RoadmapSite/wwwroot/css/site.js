document.addEventListener("click", function (event) {
    if (!event.target.matches(".vote-button")) {
        return;
    }

    event.target.classList.toggle("voted");
});