$(function () {
    $("table tbody").sortable();
    $("table tbody").disableSelection();
});

function stateChange() {
    setTimeout(function () {
            location.reload();
    }, 1000);
}

function saveSortedQuestions() {
    var questions = $.map($("tr.ui-state-default"), function (item, index) {
        var questionDetail = new Object();
        questionDetail.Id = parseInt($(item).attr("id"));
        questionDetail.Priority = index + 1;
        return questionDetail;  
    });

    var jsonQuestions = JSON.stringify(questions);
    $.ajax({
        type: "POST",
        contentType: 'application/json',
        data: jsonQuestions,
        url: 'https://localhost:44352/api/values/SortPanels?questions=',
        dataType: "json",
        traditional: true,
        succes: stateChange(),
        error: stateChange()
    });
};

function refreshForm(id) {
    var question = document.getElementById("Question" + id);
    var title = document.getElementById("Title" + id);
    var questionType = document.getElementById("QuestionType" + id);
    var position = document.getElementById("Position" + id);

    var questionTarget = document.getElementById("QuestionData_Question");
    var titleTarget = document.getElementById("QuestionData_Title");
    var questionTypeTarget = document.getElementById("QuestionData_QuestionType");
    var positionTarget = document.getElementById("QuestionData_Position");
    var idTarget = document.getElementById("QuestionData_QuestionDataId");

    questionTarget.value = question.innerHTML;
    titleTarget.value = title.innerHTML;
    questionTypeTarget.value = questionType.innerHTML;
    positionTarget.value = position.innerHTML;
    idTarget.value = id;
}