mergeInto(LibraryManager.library, {

    ExecutionFinished: function (completed) {
        ExecutionFinishedBrowser(completed);
    }
});