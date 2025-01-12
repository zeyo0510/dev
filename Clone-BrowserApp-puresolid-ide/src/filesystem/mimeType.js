var MimeType = {
    fromExtenstion: function (ex) {
        switch (ex) {
            case 'json': return 'application/json'; break;
            case 'js': return 'text/javascript'; break;
            case 'htm':
            case 'html': return 'text/html'; break;
            case 'xml': return 'text/xml'; break;
            case 'css': return 'text/css'; break;
            case 'jpg': return 'image/jpeg'; break;
            case 'jpeg': return 'image/jpeg'; break;
            case 'png': return 'image/png'; break;
            case 'bmp': return 'image/bmp'; break;
            case 'gif': return 'image/gif'; break;
            case 'ico': return 'image/icon'; break;
            case 'txt': return 'text/plain'; break;
            case 'php': return 'application/x-httpd-php'; break;
            default: return undefined; break;
        }
    },
    isImage: function(ext) {

switch (ext.toLowerCase()) {
    case 'jpg':
    case 'gif':
    case 'bmp':
    case 'png':
    case 'ico':
        //etc
        return true;
}
return false;
}
}