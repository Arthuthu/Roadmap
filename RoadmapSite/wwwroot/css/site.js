function toggleVoteButtonClass(button) {
    if (button.classList.contains("vote-button-voted")) {
        button.classList.remove("vote-button-voted");
        button.classList.add("vote-button");
    } else {
        button.classList.remove("vote-button");
        button.classList.add("vote-button-voted");
    }
}

function toggleVoteButtonDetailsClass(button) {
    if (button.classList.contains("vote-button-voted-details")) {
        button.classList.remove("vote-button-voted-details");
        button.classList.add("vote-button-details");
    } else {
        button.classList.remove("vote-button-details");
        button.classList.add("vote-button-voted-details");
    }
}

//var isVoted = false;
//var voteCountEl = document.getElementById("vote-count");

//function toggleVote() {
//    if (isVoted) {
//        voteCountEl.textContent = parseInt(voteCountEl.textContent) - 1;
//    } else {
//        voteCountEl.textContent = parseInt(voteCountEl.textContent) + 1;
//    }
//    isVoted = !isVoted;
//}
