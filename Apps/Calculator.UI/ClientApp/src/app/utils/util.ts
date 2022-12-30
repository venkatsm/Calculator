export class Util {
  getSessionId(): any {
    var date = (new Date()).toISOString();
    
    return date.toString().replace(/[&\/\\#,+()$~%.'":*?<>{}]/g, '').replace(/[ ()]/g, '');
  }
}
