var X = "X"
var O = "O"
var X_Win = 0;
var O_Win = 0;
var PlayedTurn = 0;
$("#XWin").prop('disabled', true);
$("#OWin").prop('disabled', true);

$(".btn-tic-tac-toe").click(function () {
}, function () {
    $(this).removeClass("btn-default");
    $(this).prop('disabled', true);
    if (PlayedTurn % 2 == 0) {
        $(this).addClass("btn-primary");
        $(this).addClass("X");
        $(this).val("X");
        PlayedTurn++;
    }
    else {
        $(this).addClass("btn-info");
        $(this).addClass("O");
        $(this).val("O");
        PlayedTurn++;
    }
    CheckWin();
});

$("#btnTicTacToeNewGame").click(function () {
    $("#TicTacToe .btn-tic-tac-toe").val("•");
    $("#TicTacToe .btn-tic-tac-toe").removeClass("btn-info");
    $("#TicTacToe .btn-tic-tac-toe").removeClass("btn-primary");
    $("#TicTacToe .btn-tic-tac-toe").removeClass("O");
    $("#TicTacToe .btn-tic-tac-toe").removeClass("X");
    $("#TicTacToe .btn-tic-tac-toe").addClass("btn-default");
    $("#TicTacToe .btn-tic-tac-toe").prop('disabled', false);
    PlayedTurn = 0;
});

function CheckWin() {
    if ($("#one").hasClass('O') &&
        $("#two").hasClass('O') &&
        $("#three").hasClass('O') ||
        $("#four").hasClass('O') &&
        $("#five").hasClass('O') &&
        $("#six").hasClass('O') ||
        $("#seven").hasClass('O') &&
        $("#eight").hasClass('O') &&
        $("#nine").hasClass('O') ||
        $("#one").hasClass('O') &&
        $("#four").hasClass('O') &&
        $("#seven").hasClass('O') ||
        $("#two").hasClass('O') &&
        $("#five").hasClass('O') &&
        $("#eight").hasClass('O') ||
        $("#three").hasClass('O') &&
        $("#six").hasClass('O') &&
        $("#nine").hasClass('O') ||
        $("#one").hasClass('O') &&
        $("#five").hasClass('O') &&
        $("#nine").hasClass('O') ||
        $("#three").hasClass('O') &&
        $("#five").hasClass('O') &&
        $("#seven").hasClass('O')) {
        alert('Victoire O!')
        $("#TicTacToe .btn-tic-tac-toe").prop('disabled', true);
        PlayedTurn = 0;
        O_Win++;
        $("#OWin").val(O_Win);

    }

    else if ($("#one").hasClass('X') &&
        $("#two").hasClass('X') &&
        $("#three").hasClass('X') ||
        $("#four").hasClass('X') &&
        $("#five").hasClass('X') &&
        $("#six").hasClass('X') ||
        $("#seven").hasClass('X') &&
        $("#eight").hasClass('X') &&
        $("#nine").hasClass('X') ||
        $("#one").hasClass('X') &&
        $("#four").hasClass('X') &&
        $("#seven").hasClass('X') ||
        $("#two").hasClass('X') &&
        $("#five").hasClass('X') &&
        $("#eight").hasClass('X') ||
        $("#three").hasClass('X') &&
        $("#six").hasClass('X') &&
        $("#nine").hasClass('X') ||
        $("#one").hasClass('X') &&
        $("#five").hasClass('X') &&
        $("#nine").hasClass('X') ||
        $("#three").hasClass('X') &&
        $("#five").hasClass('X') &&
        $("#seven").hasClass('X')) {
        alert('Victoire X!')
        $("#TicTacToe .btn-tic-tac-toe").prop('disabled', true);
        PlayedTurn = 0;
        X_Win++;
        $("#XWin").val(X_Win);
    }
    else if (PlayedTurn == 9) {
        alert('Égalité!');
        PlayedTurn = 0;
    }
}
