window.dialogHelper = {
    closeDialog: function (dialogId) {
        const dialog = document.getElementById(dialogId);
        if (dialog) dialog.close();
    }
};
