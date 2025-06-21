window.initializeCodeMirror = function (id) {
    let textArea = document.getElementById(id);
    if (!textArea) {
        console.error("Error: No se encontró el elemento con ID " + id);
        return;
    }

    window.codeMirrorEditor = CodeMirror.fromTextArea(textArea, {
        mode: "text/x-csharp",
        theme: "dracula",
        lineNumbers: true,
        autoCloseBrackets: true,
        matchBrackets: true,
        extraKeys: { "Ctrl-Space": "autocomplete" }
    });

    let defaultCode = `using System;
class Program {
    static void Main(string[] args) {
        Console.WriteLine("Hola desde ACC!");
    }
}`;
    window.codeMirrorEditor.setValue(defaultCode);
};

window.getCodeMirrorValue = function () {
    return window.codeMirrorEditor?.getValue?.() ?? "";
};

window.changeCodeMirrorTheme = function (theme) {
    if (window.codeMirrorEditor) {
        window.codeMirrorEditor.setOption("theme", theme);
    }
};
