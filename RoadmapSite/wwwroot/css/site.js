function addClickListener() {
    var voteButton = document.querySelector(".vote-button");
    if (!voteButton) {
        return setTimeout(addClickListener, 100);
    }

    var voted = localStorage.getItem("voted") === "true";

    if (voted) {
        voteButton.classList.add("voted");
    }

    voteButton.addEventListener("click", function () {
        voted = !voted;
        localStorage.setItem("voted", voted);
        voteButton.classList.toggle("voted");
    });
}

document.addEventListener("DOMContentLoaded", addClickListener);