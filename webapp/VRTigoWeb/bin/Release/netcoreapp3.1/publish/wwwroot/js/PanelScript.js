
$(function () {
    $("table tbody").sortable();
    $("table tbody").disableSelection();
});

document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('NewTypeName').style = 'display: none;';
}, false);

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
    var questionType = "";
    if (document.getElementById("QuestionType" + id) != null) {
        questionType = document.getElementById("QuestionType" + id).innerHTML;
    } else {
        questionType = "1"
    }

    var position = document.getElementById("Position" + id);
    var previous = document.getElementById("KeepPrevious" + id);

    var questionTarget = document.getElementById("QuestionData_Question");
    var titleTarget = document.getElementById("QuestionData_Title");
    var questionTypeTarget = document.getElementById("SelectedItem");
    var positionTarget = document.getElementById("QuestionData_Position");
    var idTarget = document.getElementById("QuestionData_QuestionDataId");
    var previousTarget = document.getElementById("QuestionData_KeepPreviousValue");

    questionTarget.value = question.innerHTML;
    titleTarget.value = title.innerHTML;
    questionTypeTarget.selectedIndex = ""+(questionType-1);
    positionTarget.value = position.innerHTML;
    idTarget.value = id;
    switch (previous.innerHTML.toLowerCase())
    {
        case "false": previousTarget.checked = false; break;
        case "true": previousTarget.checked = true; break;
    }
}

function newQuestion(posId) {
    var questionTarget = document.getElementById("QuestionData_Question");
    var titleTarget = document.getElementById("QuestionData_Title");
    var questionTypeTarget = document.getElementById("SelectedItem");
    var positionTarget = document.getElementById("QuestionData_Position");
    var idTarget = document.getElementById("QuestionData_QuestionDataId");
    var previousTarget = document.getElementById("QuestionData_KeepPreviousValue");
    var newQuestionId = document.getElementById("NewQuestion");

    newQuestionId.checked = true;
    questionTarget.value = "";
    titleTarget.value = "";
    questionTypeTarget.selectedIndex = 1;
    positionTarget.value = posId;
    idTarget.value = 0;
    previousTarget.checked = false;
}

function newType() {
    document.getElementById('NewTypeName').style = 'display: block;';
    document.getElementById('SelectedItem').style = 'display: none;';
    document.getElementById('createQuestionType').style = 'display: none;';
    document.getElementById("NewType").checked = true;
}

function deleteType() {
    var questionType = document.getElementById("SelectedItem").value;

    $.ajax({
        type: "GET",
        url: 'https://localhost:44352/Settings/DeleteQuestionType/'+questionType,
        traditional: true,
        succes: stateChange(),
        error: stateChange()
    });
};