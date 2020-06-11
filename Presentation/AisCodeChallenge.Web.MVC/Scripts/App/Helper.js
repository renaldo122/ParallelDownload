var View = (function () {

    var _ajax = function (options) {
        return new window.Promise(function (resolve, reject) {
            $.ajax(options)
                .done(resolve)
                .fail(reject);
        });
    }

    function isEmpty(val) {
        return (val === undefined || val == null || val.length <= 0 || val <= 0) ? true : false;
    }

    Succes = function (response,el) {
        $("#" + el.container).html(response); 
    }

    failure = function (err) {
        console.log(err);
    }


    var dependancyObject = {

        RefreshFileList: function () {
            return {
                globalName: "fileList",
                container: "_fileList",
                url: "/File/RefreshFileList/"
            };
        },

    };

    DownloadingObject = function () {
        return {
            default: {
                IsFilesdownloaded: false,
                confirm: true,
            },
            handler: {
                get: function (obj, prop) {
                    return obj[prop];
                },
                set: function (obj, prop, value) {
                    if (arguments.callee.caller !== null) {
                        if (prop == "clear") {
                            this.reset(obj);
                            return;
                        }
                        obj[prop] = value;
                    }

                },
                reset: function (obj, statesToKeep = []) {
                    var newObj = DownloadingObject().default;
                    Object.keys(newObj)
                        .filter(function (e) {
                            return this.indexOf(e) < 0;
                        },
                            statesToKeep).forEach(function (key, index) {
                                obj[key] = newObj[key];
                            });
                }
            }
        }
    };

    _push = function (obj, _context) {
        Object.keys(obj).forEach(function (key, index) {
            _context[key] = obj[key];
        });
    }
    _pushValue = function (param, value, _context) {
        _context[param] = value;
    }

    var _proxy_instances = [],
        _proxyCreator = function (objToProxy, data, handler) {
            return (typeof _proxy_instances[objToProxy] === "undefined") ? _proxy_instances[objToProxy] = new Proxy(data, handler) : _proxy_instances[objToProxy];
        }

    var _currentState = {
        DownloadingObject: {
            data: DownloadingObject().default,
            handler: DownloadingObject().handler,
            _proxy: function () {
                return _proxyCreator("DownloadingObject", this.data, this.handler);
            },
            push: function (obj) {
                _push(obj, this._proxy());
            },
            pushValue: function (param, value) {
                _pushValue(param, value, this._proxy());
            },
            _get: function () {
                return this._proxy();
            },
            clear: function () {
                this.pushValue("clear", true);
            }
        },
    }

    return {

        load: function (el, data = {},showLoadingPanel = true) {
            if (typeof dependancyObject[el] !== "undefined") {
                el = dependancyObject[el]();
                _ajax({
                    type: el.type || "POST",
                    url: el.url,
                    data: data
                }).then(function (response) {  Succes(response, el); }, failure);
            }
        },

        PrepareDownloadData :function (fields, fileData) {
            var jsonObj = {};
            var jsonChildData = {};
            var jsonParentData = [];

            for (var i = 0; i < fileData.length; i++) {
                jsonChildData[fields[0]] = fileData[i];
                jsonObj["DownloadData"] = jsonChildData;
                jsonParentData.push(jsonObj["DownloadData"]);
                jsonChildData = {};
            }

            return jsonParentData;
        },

        download_multiple_files : function (files) {
            function download_next(i) {
                if (i >= files.length) {
                    return;
                }
                var a = document.createElement('a');
                a.href = files[i].download;
                a.target = '_parent';

                // Use a.download if available, it prevents plugins from opening.
                if ('download' in a) {
                    a.download = files[i].filename;
                }
                // Add a to the doc for click to work.
                (document.body || document.documentElement).appendChild(a);
                if (a.click) {
                    a.click(); // The click method is supported by most browsers.
                } else {
                    $(a).click(); // Backup using jquery
                }

                // Delete the temporary link.
                a.parentNode.removeChild(a);

                // Download the next file with a small timeout. The timeout is necessary
                // for IE, which will otherwise only download the first file.
                setTimeout(function () {
                    download_next(i + 1);
                }, 500);
            }
            // Initiate the first download.
            download_next(0);
        },

        SetNotification: function (message, messagetype, timeout) {
            if (isEmpty(message))
                return;
            if (timeout == null) timeout = 2000;
            var options = {
                timeOut: timeout,
                closeButton: true,
                progressBar: true,
                preventDuplicates: true
            }
            if ((typeof message) == 'string') {
                switch (messagetype) {
                    case "success":
                        toastr.success(message, '', options);
                        break;
                    case "error":
                        toastr.error(message, '', options);
                        break;
                    case "warning":
                        toastr.warning(message, '', options);
                        break;
                    case "info":
                        toastr.info(message, '', options);
                        break;

                    default:
                        toastr.success(message, '', options);
                }
            }
            else {
                message.forEach(function (value, index) {
                    delete message[index];
                    Notification.SetNotification(value, messagetype, timeout);
                });
            }
        },

        GetDownloadingObject: function () {
            return _currentState.DownloadingObject._get();
        },
        pushState: function (stateObj, objToPush) {
            if (typeof _currentState[stateObj] !== "undefined")
                _currentState[stateObj].push(objToPush);
        },
        clearState: function (stateObj) {
            if (typeof _currentState[stateObj] !== "undefined")
                _currentState[stateObj].clear();
        },
    }

})();





