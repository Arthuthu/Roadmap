/* Change Button Class */

function toggleVoteButtonClass(button) {
    if (button.classList.contains("vote-button-voted")) {
        button.classList.remove("vote-button-voted");
        button.classList.add("vote-button");
    } else {
        button.classList.remove("vote-button");
        button.classList.add("vote-button-voted");
    }
}

function toggleComentarioVoteButtonClass(button) {
    if (button.classList.contains("comentario-vote-button-voted")) {
        button.classList.remove("comentario-vote-button-voted");
        button.classList.add("comentario-vote-button");
    } else {
        button.classList.remove("comentario-vote-button");
        button.classList.add("comentario-vote-button-voted");
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


/* Clear Text Input */

function clearNodeInput() {
    document.getElementById("node-input").value = "";
}

function clearComentarioInput() {
    document.getElementById("comment-input").value = "";
}