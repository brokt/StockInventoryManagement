function forgeryToken() {
    return kendo.antiForgeryTokens();
}

function onRequestEnd(e) {
    if ((e.type != null && e.type != "read")) {
        e.sender.read();
    } else {
        return;
    }
}

function onErrorGrid(e, status) {
    debugger;
    console.log(e);
    var message = "";
    if (e.xhr.responseJSON != null) {
        $.each(e.xhr.responseJSON.Errors.exception, function (key, value) {

            message += value.concat("\n");

        });
    } else {
        message += e.errorThrown;
    }

    $.notifyPopUp("Hata!!", message, "errorNotify");

    //alert(message);

}
function editItem(e) {
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var baseUrl = $('#grid')[0].baseURI.split('#')[0];
    var editUrl = baseUrl + '/edit?id=' + dataItem.Id;
    window.location = editUrl;
}

function detailItem(e) {
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var baseUrl = $('#grid')[0].baseURI.split('#')[0];
    var editUrl = baseUrl + '/details?id=' + dataItem.Id;
    window.location = editUrl;
}
$(document).ready(function () {
    initForm();

    $(".copy-email").click(function () {
        var email = $(this).closest(".copy-text").find("p.cus-email").text().trim();
        copyTextToClipboard(email);
        handleCopyAnimation($(this).closest(".copy-text"));
    });

    $(".copy-phone").click(function () {
        document.documentElement.style.setProperty('--copy-text-email-right-after-val', '70px');
        var phone = $(this).closest(".copy-text").find("p.cus-phone").text().trim();
        copyTextToClipboard(phone);
        var copyText = $(this).closest(".copy-text");
        handleCopyAnimation(copyText);
        setTimeout(function () {
            document.documentElement.style.setProperty('--copy-text-email-right-after-val', '100px');
        }, 2500);
    });

    function copyTextToClipboard(text) {
        var tempInput = $("<input>");
        $("body").append(tempInput);
        tempInput.val(text).select();
        document.execCommand("copy");
        tempInput.remove();
    }

    function handleCopyAnimation(element) {
        element.addClass("active");
        setTimeout(function () {
            element.removeClass("active");
        }, 2500);
    }
})

function initForm() {
    $('.btn-save').click(function () {
        $('.main-form').submit();
    });

    $('.btn-delete').click(function () {
        $('.delete-form').submit();
    });
}
function kFormatter(num) {
    return Math.abs(num) > 999 ? Math.sign(num) * ((Math.abs(num) / 1000).toFixed(1)) + 'K' : Math.sign(num) * Math.abs(num)
}
function saveEntity() {
    if ($('#form-main').length == 0) {
        alert("Form main not exists!");
        return;
    }

    $('#form-main').submit();
}

function deleteEntity() {
    if ($('#form-delete').length == 0) {
        alert("Form delete not exists!");
        return;
    }

    $('#form-delete').append('<div id="delete-dialog"></div>');

    $("#delete-dialog").kendoDialog({
        width: "300px",
        buttonLayout: "normal",
        title: "Uyarı!",
        closable: true,
        modal: true,
        content: "<p>Silmek istediğinize emin misiniz?<p>",
        actions: [
            { text: 'İptal' },
            {
                text: 'Sil',
                primary: true,
                action: function () {
                    $('#form-delete').submit();
                }
            }
        ],
        close: function () {
            $('#delete-dialog').remove();
        }
    });
}

function calculateSum(array, property) {
    const total = array.reduce((accumulator, object) => {
        return accumulator + object[property];
    }, 0);

    return total;
}