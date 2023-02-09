function toggleVoteButtonClass(button) {
    if (button.classList.contains("vote-button-voted")) {
        button.classList.remove("vote-button-voted");
        button.classList.add("vote-button");
    } else {
        button.classList.remove("vote-button");
        button.classList.add("vote-button-voted");
    }
}
