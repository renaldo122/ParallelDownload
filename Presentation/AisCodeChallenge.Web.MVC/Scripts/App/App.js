
setInterval(function () {
    View.load('RefreshFileList', {}, false)
}, 300000);

window.onload = function (e) {
    var IsFilesdownloaded = (localStorage.getItem('IsFilesdownloaded') == 'true');
    View.pushState("DownloadingObject", { IsFilesdownloaded: IsFilesdownloaded });
    App.ShowHideCancelGroup(IsFilesdownloaded);
};
window.onbeforeunload = function () {
    if (View.GetDownloadingObject().confirm && View.GetDownloadingObject().IsFilesdownloaded) {
        return "Files are being downloaded, do you want to exit?";
    }
};

var App = (function () {

    function refreshFiles() {
        View.load('RefreshFileList', {}, false);
    }

    function downloadFiles() {
        var fields = ["AbsoluteUri"];
        var filesJson = [];
        $('#fileListTable tr').each(function () {
            filesJson.push($(this).closest('tr').find('td:nth-child(3)').text());
        });
        if (filesJson.length === 0) {
            View.SetNotification(enums.PleaseSelectAnItemInGrid, 'warning', 3500);
            return;
        }
        var downloadData = JSON.stringify(View.PrepareDownloadData(fields, filesJson));
        var degreeOfParallelism = $('#degreeOfParallelism').val();
        App.ShowHideCancelGroup(true);
        $.ajax({
            cache: false,
            type: 'GET',
            url: '/File/DownloadFiles',
            contentType: 'application/json; charset=utf-8',
            data: {
                "downloadData": downloadData,
                "degreeOfParallelism": Number(degreeOfParallelism)

            },
            dataType: "json",
            success: function (response) {
                var res = JSON.parse(response.data);
                View.SetNotification(res.Message, res.Success ? "success" : "warning", 3500);
                App.ShowHideCancelGroup(false);
                if (res.Success) {
                    var filesToDownload = res.CustomAction.FileName.split(";");
                    var i;
                    var filesJson = [];
                    for (i = 0; i < filesToDownload.length - 1; i++) {
                        filesJson.push({
                            download: "File/DownloadFileToUser?fileName=" + filesToDownload[i],
                            filename: filesToDownload[i]
                        });
                    }
                    View.download_multiple_files(filesJson);
                }
            },
            error: function (response) {
                Notification.SetNotification(enums.ErrorMessage, 'error', 3500);
                App.ShowHideCancelGroup(false);
            }
        });
    }

    function cancelDownload() {
        $.ajax({
            type: 'GET',
            url: '/File/CancelDownload',
            contentType: 'application/json; charset=utf-8',
            data: {},
            dataType: "json",
            success: function (response) {
                var res = JSON.parse(response.data);
                View.SetNotification(res.Message, res.Success ? "success" : "warning", 3500);
                App.ShowHideCancelGroup(false);
            },
            error: function (response) {
                Notification.SetNotification(enums.ErrorMessage, 'error', 3500);
                App.ShowHideCancelGroup(false);
            }
        });
    }

    function showHideCancelGroup(display) {
        try {
            var groupCancel = document.getElementById("IdFrmGroupCancel");
            var btnDwonload = document.getElementById("btnDownload");
            if (groupCancel !== null || groupCancel !== undefined) {
                if (display) {
                    groupCancel.style.visibility = "visible";
                    View.pushState("DownloadingObject", { IsFilesdownloaded: display });
                    localStorage.setItem('IsFilesdownloaded', display);
                } else {
                    groupCancel.style.visibility = "hidden";
                    View.pushState("DownloadingObject", { IsFilesdownloaded: display });
                    localStorage.setItem('IsFilesdownloaded', display);
                }
            }
            if (btnDwonload !== null || btnDwonload !== undefined) {
                if (display) {
                    if (btnDwonload !== null || btnDwonload !== undefined) {
                        btnDwonload.disabled = display;
                    }
                } else {
                    if (btnDwonload !== null || btnDwonload !== undefined) {
                        btnDwonload.disabled = display;
                    }
                }
            }
        } catch (ex) {
            //Log error
        }
    }

    function handleOnClose() {
        View.pushState("DownloadingObject", { confirm: false });
    }

    return {
        RefreshFiles: refreshFiles,
        DownloadFiles: downloadFiles,
        CancelDownload: cancelDownload,
        ShowHideCancelGroup: showHideCancelGroup,
        HandleOnClose: handleOnClose,
    };
})();
