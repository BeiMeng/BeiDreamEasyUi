(function ($) {
    $.webuploader = function () {
        //上传控件集合
        var uploaderList = [];

        //获取上传控件
        function getUploader(name) {
            if (!name && uploaderList.length === 1)
                return uploaderList[0].control;
            for (var i = 0; i < uploaderList.length; i++) {
                var item = uploaderList[i];
                if (item.name === name)
                    return item.control;
            }
            return null;
        }

        return {
            create: function (opts) {
                ///	<summary>
                ///	创建上传控件
                ///	</summary>
                ///	<param name="opts" type="Object">
                ///	1. name:控件名称
                /// 2. 其它参数见官方API
                ///	</param>
                var options;
                initOptions();
                var uploader = WebUploader.create(options);
                registerEvents();
                addToList();

                //初始化配置项
                function initOptions() {
                    options = {
                        swf: '/Scripts/WebUploader/Uploader.swf',
                        fileVal: opts.name,
                        pick: { id: "#filePicker", multiple: false }
                    };
                    $.extend(options, opts);
                }

                //注册事件
                function registerEvents() {
                    registeBeforeFileQueued();
                    registerFileQueued();
                    registerFilesQueued();
                    registerFileDequeued();
                    registeReset();
                    registeStartUpload();
                    registeStopUpload();
                    registeUploadFinished();
                    registeUploadStart();
                    registeUploadBeforeSend();
                    registeUploadAccept();
                    registeUploadProgress();
                    registeUploadError();
                    registeUploadSuccess();
                    registeUploadComplete();
                    registeError();

                    //当文件被加入队列之前触发，此事件的handler返回值为false，则此文件不会被添加进入队列
                    function registeBeforeFileQueued() {
                        if (opts.beforeFileQueued)
                            uploader.on('beforeFileQueued', opts.beforeFileQueued);
                    }

                    //当文件被加入队列以后触发
                    function registerFileQueued() {
                        if (opts.fileQueued)
                            uploader.on('fileQueued', opts.fileQueued);
                    }

                    //当一批文件添加进队列以后触发
                    function registerFilesQueued() {
                        if (opts.filesQueued)
                            uploader.on('filesQueued', opts.filesQueued);
                    }

                    //当文件被移除队列后触发
                    function registerFileDequeued() {
                        if (opts.fileDequeued)
                            uploader.on('fileDequeued', opts.fileDequeued);
                    }

                    //重置的时候触发
                    function registeReset() {
                        if (opts.reset)
                            uploader.on('reset', opts.reset);
                    }

                    //当开始上传时触发
                    function registeStartUpload() {
                        if (opts.startUpload)
                            uploader.on('startUpload', opts.startUpload);
                    }

                    //当上传暂停时触发
                    function registeStopUpload() {
                        if (opts.stopUpload)
                            uploader.on('stopUpload', opts.stopUpload);
                    }

                    //当所有文件上传结束时触发
                    function registeUploadFinished() {
                        if (opts.uploadFinished)
                            uploader.on('uploadFinished', opts.uploadFinished);
                    }

                    //某个文件开始上传前触发，一个文件只会触发一次
                    function registeUploadStart() {
                        if (opts.uploadStart)
                            uploader.on('uploadStart', opts.uploadStart);
                    }

                    //当某个文件的分块在发送前触发，主要用来询问是否要添加附带参数，大文件在开起分片上传的前提下此事件可能会触发多次
                    function registeUploadBeforeSend() {
                        if (opts.uploadBeforeSend)
                            uploader.on('uploadBeforeSend', opts.uploadBeforeSend);
                    }

                    //当某个文件上传到服务端响应后，会派送此事件来询问服务端响应是否有效。如果此事件handler返回值为false, 则此文件将派送server类型的uploadError事件
                    function registeUploadAccept() {
                        if (opts.uploadAccept)
                            uploader.on('uploadAccept', opts.uploadAccept);
                    }

                    //上传过程中触发，携带上传进度
                    function registeUploadProgress() {
                        if (opts.uploadProgress)
                            uploader.on('uploadProgress', opts.uploadProgress);
                    }

                    //当文件上传出错时触发
                    function registeUploadError() {
                        if (opts.uploadError)
                            uploader.on('uploadError', opts.uploadError);
                    }

                    //当文件上传成功时触发
                    function registeUploadSuccess() {
                        if (opts.uploadSuccess)
                            uploader.on('uploadSuccess', opts.uploadSuccess);
                    }

                    //不管成功或者失败，文件上传完成时触发
                    function registeUploadComplete() {
                        if (opts.uploadComplete)
                            uploader.on('uploadComplete', opts.uploadComplete);
                    }

                    //发生错误时触发
                    function registeError() {
                        if (opts.error)
                            uploader.on('error', opts.error);
                    }
                }

                //添加到上传控件集合
                function addToList() {
                    var name = opts.name;
                    name = name || $.newGuid("");
                    uploaderList.push({ name: name, control: uploader });
                }
            },
            //初始化上传控件
            initControls: function () {
                uploaderList = [];
                var uploader = $("div[class='web-uploader']");
                if (!uploader || uploader.length === 0)
                    return;
                uploader.each(function () {
                    var options = $.toJsonObject($(this).attr("data-options"));
                    $.webuploader.create(options);
                });
            },
            upload: function (name) {
                ///	<summary>
                ///	上传
                ///	</summary>
                ///	<param name="name" type="String">
                ///	上传控件名称
                ///	</param>
                var uploader = getUploader(name);
                if (!uploader)
                    return;
                uploader.upload();
            },
            uploadImages: function (opts) {
                ///	<summary>
                ///	上传图片集合
                ///	</summary>
                ///	<param name="opts" type="Object">
                ///	配置参数
                ///	</param>
                var $wrap = $('#uploader'),
                // 图片容器
                $queue = $('<ul class="filelist"></ul>')
                    .appendTo($wrap.find('.queueList')),
                // 状态栏，包括进度和控制按钮
                $statusBar = $wrap.find('.statusBar'),
                // 文件状态信息
                $info = $statusBar.find('.info'),
                // 上传按钮
                $upload = $wrap.find('.uploadBtn'),
                // 没选择文件之前的内容。
                $placeHolder = $wrap.find('.placeholder'),
                // 总体进度条
                $progress = $statusBar.find('.progress').hide(),
                // 添加的文件数量
                fileCount = 0,
                // 添加的文件总大小
                fileSize = 0,
                // 优化retina, 在retina下这个值是2
                ratio = window.devicePixelRatio || 1,
                // 缩略图大小
                thumbnailWidth = opts.thumbnailWidth * ratio,
                thumbnailHeight = opts.thumbnailHeight * ratio,
                // 可能有pedding, ready, uploading, confirm, done.
                state = 'pedding',
                // 所有文件的进度信息，key为file id
                percentages = {},
                supportTransition = (function () {
                    var s = document.createElement('p').style,
                        r = 'transition' in s ||
                              'WebkitTransition' in s ||
                              'MozTransition' in s ||
                              'msTransition' in s ||
                              'OTransition' in s;
                    s = null;
                    return r;
                })(),
                // WebUploader实例
                uploader;

                //检测浏览器是否支持
                if (!WebUploader.Uploader.support()) {
                    alert('Web Uploader 不支持您的浏览器！如果你使用的是IE浏览器，请尝试升级 flash 播放器');
                    throw new Error('Web Uploader 不支持您的浏览器！如果你使用的是IE浏览器，请尝试升级 flash 播放器');
                }

                //设置MVC防伪标记
                opts.formData.__RequestVerificationToken = $.getAntiForgeryToken();

                // 实例化
                uploader = WebUploader.create({
                    pick: {
                        id: '#filePicker',
                        label: '点击选择图片'
                    },
                    dnd: '#uploader .queueList',
                    accept: {
                        title: '图片',
                        extensions: 'gif,jpg,jpeg,bmp,png'
                    },
                    swf: '/Scripts/WebUploader/Uploader.swf',
                    disableGlobalDnd: true,
                    chunked: true,
                    server: opts.server,
                    fileNumLimit: opts.fileNumLimit,
                    fileSizeLimit: opts.fileSizeLimit,
                    fileSingleSizeLimit: opts.fileSingleSizeLimit,
                    formData: opts.formData
                });

                // 添加“继续添加”的按钮，
                uploader.addButton({
                    id: '#filePicker2',
                    label: '继续添加'
                });

                // 当有文件添加进来时执行，负责view的创建
                function addFile(file) {
                    var $li = $('<li id="' + file.id + '">' +
                            '<p class="title">' + file.name + '</p>' +
                            '<p class="imgWrap"></p>' +
                            '<p class="progress"><span></span></p>' +
                            '</li>'),

                        $btns = $('<div class="file-panel">' +
                            '<span class="cancel">删除</span>' +
                            '<span class="rotateRight">向右旋转</span>' +
                            '<span class="rotateLeft">向左旋转</span></div>').appendTo($li),
                        $prgress = $li.find('p.progress span'),
                        $wrap = $li.find('p.imgWrap'),
                        $info = $('<p class="error"></p>'),

                        showError = function (code) {
                            switch (code) {
                                case 'exceed_size':
                                    text = '文件大小超出';
                                    break;

                                case 'interrupt':
                                    text = '上传暂停';
                                    break;

                                default:
                                    text = '上传失败，请重试';
                                    break;
                            }

                            $info.text(text).appendTo($li);
                        };

                    if (file.getStatus() === 'invalid') {
                        showError(file.statusText);
                    } else {
                        $wrap.text('预览中');
                        uploader.makeThumb(file, function (error, src) {
                            if (error) {
                                $wrap.text('不能预览');
                                return;
                            }

                            var img = $('<img src="' + src + '">');
                            img.width(thumbnailWidth);
                            $wrap.empty().append(img);
                        }, thumbnailWidth, thumbnailHeight);

                        percentages[file.id] = [file.size, 0];
                        file.rotation = 0;
                    }

                    file.on('statuschange', function (cur, prev) {
                        if (prev === 'progress') {
                            $prgress.hide().width(0);
                        } else if (prev === 'queued') {
                            $li.off('mouseenter mouseleave');
                            $btns.remove();
                        }

                        // 成功
                        if (cur === 'error' || cur === 'invalid') {
                            console.log(file.statusText);
                            showError(file.statusText);
                            percentages[file.id][1] = 1;
                        } else if (cur === 'interrupt') {
                            showError('interrupt');
                        } else if (cur === 'queued') {
                            percentages[file.id][1] = 0;
                        } else if (cur === 'progress') {
                            $info.remove();
                            $prgress.css('display', 'block');
                        } else if (cur === 'complete') {
                            $li.append('<span class="success"></span>');
                        }

                        $li.removeClass('state-' + prev).addClass('state-' + cur);
                    });

                    $li.on('mouseenter', function () {
                        $btns.stop().animate({ height: 30 });
                    });

                    $li.on('mouseleave', function () {
                        $btns.stop().animate({ height: 0 });
                    });

                    $btns.on('click', 'span', function () {
                        var index = $(this).index(),
                            deg;
                        switch (index) {
                            case 0:
                                uploader.removeFile(file);
                                return;

                            case 1:
                                file.rotation += 90;
                                break;

                            case 2:
                                file.rotation -= 90;
                                break;
                        }
                        if (supportTransition) {
                            deg = 'rotate(' + file.rotation + 'deg)';
                            $wrap.css({
                                '-webkit-transform': deg,
                                '-mos-transform': deg,
                                '-o-transform': deg,
                                'transform': deg
                            });
                        } else {
                            $wrap.css('filter', 'progid:DXImageTransform.Microsoft.BasicImage(rotation=' + (~~((file.rotation / 90) % 4 + 4) % 4) + ')');
                        }
                    });
                    $li.appendTo($queue);
                }

                // 负责view的销毁
                function removeFile(file) {
                    var $li = $('#' + file.id);
                    delete percentages[file.id];
                    updateTotalProgress();
                    $li.off().find('.file-panel').off().end().remove();
                }

                function updateTotalProgress() {
                    var loaded = 0,
                        total = 0,
                        spans = $progress.children(),
                        percent;
                    $.each(percentages, function (k, v) {
                        total += v[0];
                        loaded += v[0] * v[1];
                    });
                    percent = total ? loaded / total : 0;
                    spans.eq(0).text(Math.round(percent * 100) + '%');
                    spans.eq(1).css('width', Math.round(percent * 100) + '%');
                    updateStatus();
                }

                function updateStatus() {
                    var text = '', stats;
                    if (state === 'ready') {
                        text = '已添加' + fileCount + '张图片，共' + WebUploader.formatSize(fileSize);
                    } else if (state === 'confirm') {
                        stats = uploader.getStats();
                        if (stats.uploadFailNum) {
                            text = '已成功上传' + stats.successNum + '张，' +
                                stats.uploadFailNum + '张上传失败，<a class="retry" href="#">重新上传</a>失败图片';
                        }
                    } else {
                        stats = uploader.getStats();
                        text = '共' + fileCount + '张（' +
                                WebUploader.formatSize(fileSize) +
                                '），已上传' + stats.successNum + '张';

                        if (stats.uploadFailNum) {
                            text += '，失败' + stats.uploadFailNum + '张';
                        }
                    }
                    $info.html(text);
                }

                function setState(val) {
                    var file, stats;
                    if (val === state) {
                        return;
                    }
                    $upload.removeClass('state-' + state);
                    $upload.addClass('state-' + val);
                    state = val;
                    switch (state) {
                        case 'pedding':
                            $placeHolder.removeClass('element-invisible');
                            $queue.parent().removeClass('filled');
                            $queue.hide();
                            $statusBar.addClass('element-invisible');
                            $wrap.find(".queueList").css("height", "440px");
                            uploader.refresh();
                            break;
                        case 'ready':
                            $placeHolder.addClass('element-invisible');
                            $wrap.find(".queueList").css("height", "365px");
                            $('#filePicker2').removeClass('element-invisible');
                            $queue.parent().addClass('filled');
                            $queue.show();
                            $statusBar.removeClass('element-invisible');
                            uploader.refresh();
                            break;
                        case 'uploading':
                            $('#filePicker2').addClass('element-invisible');
                            $progress.show();
                            $upload.text('暂停上传');
                            break;
                        case 'paused':
                            $progress.show();
                            $upload.text('继续上传');
                            break;
                        case 'confirm':
                            $progress.hide();
                            $upload.text('开始上传').addClass('disabled');
                            stats = uploader.getStats();
                            if (stats.successNum && !stats.uploadFailNum) {
                                setState('finish');
                                return;
                            }
                            break;
                        case 'finish':
                            stats = uploader.getStats();
                            if (stats.successNum) {
                                if (opts.uploadFinished)
                                    opts.uploadFinished();
                            } else {
                                state = 'done';
                                location.reload();
                            }
                            break;
                    }
                    updateStatus();
                }

                uploader.onUploadProgress = function (file, percentage) {
                    var $li = $('#' + file.id),
                        $percent = $li.find('.progress span');
                    $percent.css('width', percentage * 100 + '%');
                    percentages[file.id][1] = percentage;
                    updateTotalProgress();
                };

                uploader.onFileQueued = function (file) {
                    fileCount++;
                    fileSize += file.size;
                    if (fileCount === 1) {
                        $placeHolder.addClass('element-invisible');
                        $statusBar.show();
                    }
                    addFile(file);
                    setState('ready');
                    updateTotalProgress();
                };

                uploader.onFileDequeued = function (file) {
                    fileCount--;
                    fileSize -= file.size;
                    if (!fileCount) {
                        setState('pedding');
                    }
                    removeFile(file);
                    updateTotalProgress();
                };

                uploader.on('all', function (type) {
                    var stats;
                    switch (type) {
                        case 'uploadFinished':
                            setState('confirm');
                            break;
                        case 'startUpload':
                            setState('uploading');
                            break;
                        case 'stopUpload':
                            setState('paused');
                            break;
                    }
                });

                uploader.on('uploadAccept', function(file, result) {
                    return result.Code === $.easyui.state.ok;
                });

                $upload.on('click', function () {
                    if ($(this).hasClass('disabled')) {
                        return false;
                    }
                    if (state === 'ready') {
                        uploader.upload();
                    } else if (state === 'paused') {
                        uploader.upload();
                    } else if (state === 'uploading') {
                        uploader.stop();
                    }
                    return false;
                });

                $info.on('click', '.retry', function () {
                    uploader.retry();
                });

                $info.on('click', '.ignore', function () {
                });
                $upload.addClass('state-' + state);
                updateTotalProgress();
            }
        };
    }();
})(jQuery);