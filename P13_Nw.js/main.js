const os = require("os");

document.body.insertAdjacentHTML("beforeend", `<p>Running on: ${os.platform()}</p>`);
