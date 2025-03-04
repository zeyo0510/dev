(function () {
  (function () {
    var c = 20,
      a = 1280,
      b = 1024;
    window.GLIFFY = window.GLIFFY || {};
    window.GLIFFY.desktopUtils = {
      getBounds: function (f, e, h, g) {
        var d = {};
        d.width = h;
        d.height = g;
        d.width = Math.min(d.width, screen.availWidth);
        d.height = Math.min(d.height, screen.availHeight);
        if (f) {
          d.left = f;
        } else {
          d.left = Math.floor((screen.availWidth - d.width) / 2);
        }
        if (e) {
          d.top = e;
        } else {
          d.top = Math.floor((screen.availHeight - d.height) / 2);
        }
        d.left = Math.min(d.left, screen.availWidth - c);
        d.top = Math.min(d.top, screen.availHeight - c);
        d.left = Math.max(d.left, c);
        d.top = Math.max(d.top, c);
        return d;
      },
      openWindow: function (d) {
        var f = {
            url: "",
            id: null,
            type: null,
            left: null,
            top: null,
            width: 800,
            height: 600,
            callback: null,
            singleton: true,
          },
          e;
        for (e in d) {
          if (d.hasOwnProperty(e)) {
            f[e] = d[e];
          }
        }
        chrome.app.window.create(
          f.url,
          {
            id: f.id,
            bounds: GLIFFY.desktopUtils.getBounds(
              f.left,
              f.top,
              f.width,
              f.height
            ),
            singleton: f.singleton,
          },
          f.callback
        );
      },
      openAppWindow: function (d, e) {
        chrome.storage.local.get("windowSettings", function (g) {
          var f = 1;
          if (g.windowSettings && g.windowSettings.count) {
            g.windowSettings.count++;
            f = g.windowSettings.count;
          } else {
            g.windowSettings = { count: 1, width: a, height: b };
          }
          chrome.storage.local.set(g, function () {
            var h;
            if (f === 1) {
              h = "app";
            } else {
              h = null;
            }
            GLIFFY.desktopUtils.openWindow({
              url: "index.html",
              id: h,
              left: g.windowSettings.left,
              top: g.windowSettings.top,
              width: g.windowSettings.width,
              height: g.windowSettings.height,
              singleton: false,
              callback: function (i) {
                g.windowSettings.left = i.getBounds().left += 20;
                g.windowSettings.top = i.getBounds().top += 20;
                if (h) {
                  g.windowSettings.width = i.getBounds().width;
                  g.windowSettings.height = i.getBounds().height;
                }
                chrome.storage.local.set(g, function () {
                  i.contentWindow.launch = d || null;
                  i.onClosed.addListener(function () {
                    chrome.storage.local.get("windowSettings", function (j) {
                      if (j.windowSettings.count > 1) {
                        j.windowSettings.count--;
                      } else {
                        j.windowSettings.count = 1;
                      }
                      chrome.storage.local.set(j);
                    });
                  });
                  if (e) {
                    e();
                  }
                });
              },
            });
          });
        });
      },
      formatSize: function (d) {
        var e = 0;
        while (d > 1024 && e < 5) {
          d /= 1024;
          e++;
        }
        d = Math.floor(d);
        return d + " " + ["", "K", "M", "G", "T"][e] + "B";
      },
      stringify: function (d) {
        var f = [];
        for (var e in d) {
          f.push(encodeURIComponent(e) + "=" + encodeURIComponent(d[e]));
        }
        return f.join("&");
      },
      popupWindow: function (e, d) {
        var f = window.open(e, d);
        if (window.focus) {
          f.focus();
        }
        return false;
      },
    };
  })();
  (function () {
    if (!window.GLIFFY) {
      window.GLIFFY = {};
    }
    var n = {},
      h = [{ extensions: ["gon", "gliffy"] }],
      d = [{ extensions: ["gliffy"] }],
      j,
      b,
      f = null,
      c = "unsaved",
      l = 1024 * 1024,
      i = 0,
      e = 5 * l;
    function g(p) {
      return Array.prototype.slice.call(p || [], 0);
    }
    function k(p) {
      switch (p.code) {
        case FileError.QUOTA_EXCEEDED_ERR:
          return "You do not have enough space for this.";
        case FileError.NOT_FOUND_ERR:
          return "File not found.";
        case FileError.SECURITY_ERR:
          return "You do not have permissions to do this.";
        default:
          return "There was an unknown error while trying to write the file.";
      }
    }
    function m(r, p, q, s) {
      return function (u) {
        var t = e;
        if (u.code === FileError.QUOTA_EXCEEDED_ERR) {
          f = null;
          e += 10 * l;
          GLIFFY.IO.os.getFileSystem(function () {
            if (i > t) {
              r[p].apply(r, q);
            } else {
              s(500, k(u));
            }
          });
        } else {
          console.error(u);
          s(500, k(u));
        }
      };
    }
    function a(p) {
      return GLIFFY.IO.path.join(c, p.urls.tempId);
    }
    j = function (p) {
      this.__path = null;
      this.fileEntry = p;
    };
    j.prototype.getPath = function (q) {
      var p = this;
      if (this.__path !== null) {
        q(this.__path);
      } else {
        if (this.fileEntry.fullPath) {
          q(this.fileEntry.fullPath);
        } else {
          chrome.fileSystem.getDisplayPath(this.fileEntry, function (r) {
            p.__path = r;
            q(p.__path);
          });
        }
      }
    };
    j.prototype.cache = function (q) {
      var p = this;
      this.getPath(function (r) {
        n[r] = p;
        q(p);
      });
    };
    j.prototype.remove = function (q, p) {
      if (this.__path !== null) {
        delete n[this.__path];
      }
      this.fileEntry.remove(q, function (r) {
        p(500, k(r));
      });
      delete this.fileEntry;
    };
    j.prototype.getDocumentInfo = function (q) {
      var p = this;
      this.getPath(function (r) {
        n[r] = p;
        q({ urls: { path: r }, title: p.fileEntry.name, revision: 1 });
      });
    };
    j.prototype.close = function () {
      if (this.__path !== null) {
        delete n[this.__path];
      }
      delete this.fileEntry;
    };
    j.prototype.write = function (p, t, r) {
      var q = this;
      try {
        GLIFFY.IO.os.writeData(
          this.fileEntry,
          JSON.stringify(p.gon),
          "text/plain",
          function () {
            if (this.error) {
              t({ error: "Uh oh!" });
            } else {
              q.getDocumentInfo(t);
            }
          },
          function (u) {
            t({ error: "Uh oh!" });
          }
        );
      } catch (s) {
        r(500, "The file failed to save. Please contact support.");
      }
    };
    j.prototype.read = function (r, q) {
      var p = this;
      GLIFFY.IO.os.readJsonData(
        this.fileEntry,
        function (s) {
          p.getDocumentInfo(function (t) {
            t.gon = s;
            r(t);
          });
        },
        q
      );
    };
    b = function (p) {
      j.call(this, p);
    };
    b.prototype = new j();
    b.prototype.constructor = b;
    b.prototype.getDocumentInfo = function (p) {
      GLIFFY.IO.os.readJsonData(
        this.fileEntry,
        function (q) {
          p({ urls: q.urls, title: q.title, revision: q.revision });
        },
        function (q) {
          console.error(q);
          self.getPath(function (r) {
            p({
              urls: { path: r, tempId: self.fileEntry.name },
              title: self.fileEntry.name,
              revision: 0,
            });
          });
        }
      );
    };
    b.prototype.close = function () {
      delete this.fileEntry;
    };
    b.prototype.cache = function (p) {
      p(self);
    };
    b.prototype.read = function (r, q) {
      var p = this;
      GLIFFY.IO.os.readTextData(
        this.fileEntry,
        function (t) {
          if (/^\s*$/.test(t)) {
            q(404, "Backup file was not found.");
          } else {
            try {
              var s = JSON.parse(t);
              s.revision = 0;
              GLIFFY.IO.os.getModifiedDate(
                p.fileEntry,
                function (v) {
                  s.urls.modificationDate = v;
                  r(s);
                },
                q
              );
            } catch (u) {
              q(
                500,
                "The backup file could not be loaded because it is not a recognizable format."
              );
            }
          }
        },
        q
      );
    };
    b.prototype.write = function (p, s, q) {
      try {
        GLIFFY.IO.os.writeData(
          this.fileEntry,
          JSON.stringify(p),
          "text/plain",
          function () {
            if (this.error) {
              s({ error: "The backup could not be saved." });
            } else {
              s(p);
            }
          },
          function (t) {
            s({ error: "The backup could not be saved." });
          }
        );
      } catch (r) {
        q(500, "The backup could not be saved.");
      }
    };
    GLIFFY.IO = {
      path: {
        normalize: function (p) {
          return GLIFFY.IO.path.join.apply(
            GLIFFY.IO.path,
            p.replace("\\", "/").split("/")
          );
        },
        join: function () {
          var q,
            p = [],
            r;
          for (q = 0; q < arguments.length; q++) {
            r = arguments[q]
              .toString()
              .replace("\\", "/")
              .replace(/(^\/)|(\/$)/);
            if (r == "..") {
              p.pop();
            } else {
              if (r !== "") {
                p.push(r);
              }
            }
          }
          p = p.join("/");
          return p;
        },
        fileName: function (q) {
          q = q.replace("\\", "/");
          var p = q.lastIndexOf("/");
          if (p !== -1) {
            return q.substr(p + 1);
          } else {
            return q;
          }
        },
        dirPath: function (q) {
          q = q.replace("\\", "/");
          var p = q.lastIndexOf("/");
          if (p === q.length - 1) {
            q = q;
          }
          if (p !== -1) {
            if (p !== q.length - 1) {
              q = q.substr(0, p);
            }
            return q.replace("^/", "");
          } else {
            return "";
          }
        },
      },
      os: {
        requestSpace: function (q, r, p) {
          (navigator.webkitTemporaryStorage || window.webkitStorageInfo).queryUsageAndQuota(
            // window.PERSISTENT,
            function (t, s) {
              if (t + q >= s) {
                e += Math.ceil(q / (10 * l)) * l;
                f = null;
                GLIFFY.IO.os.getFileSystem(function () {
                  r();
                }, p);
              } else {
                r();
              }
            },
            function (s) {
              console.error(s);
              p(500, k(s));
            }
          );
        },
        getFileSystem: function (r, q) {
          if (f === null) {
            var p = function (s) {
              console.error(s);
              q(500, k(s));
            };
            (navigator.webkitTemporaryStorage || window.webkitStorageInfo).requestQuota(
              // window.PERSISTENT,
              e,
              function (s) {
                i = s;
                console.debug("Requested Storage: " + Math.floor(e / l) + "MB");
                console.debug("Granted   Storage: " + Math.floor(i / l) + "MB");
                window.webkitRequestFileSystem(
                  window.PERSISTENT,
                  s,
                  function (t) {
                    f = t;
                    r(f);
                  },
                  p
                );
              },
              p
            );
          } else {
            r(f);
          }
        },
        getFileEntry: function (s, p, t, q) {
          var u = GLIFFY.IO.path.fileName(s),
            r = GLIFFY.IO.path.dirPath(s);
          GLIFFY.IO.os.getDirectory(
            r,
            p,
            function (v) {
              v.getFile(
                u,
                { create: true },
                t,
                m(GLIFFY.IO.os, "getFileEntry", arguments, q)
              );
            },
            q
          );
        },
        writeData: function (s, r, u, t, q) {
          var p;
          if (typeof s === "string") {
            GLIFFY.IO.os.getFileEntry(
              s,
              null,
              function (v) {
                GLIFFY.IO.os.writeData(v, r, u, t, q);
              },
              q
            );
          } else {
            p = function (v) {
              v.createWriter(function (x) {
                var w = new Blob([r], { type: u });
                GLIFFY.IO.os.requestSpace(
                  w.size,
                  function () {
                    x.onwriteend = function (y) {
                      x.onerror = m(GLIFFY.IO.os, "writeData", arguments, q);
                      x.onwriteend = t;
                      x.seek(0);
                      x.write(w);
                    };
                    x.onerror = function (y) {
                      console.error(y);
                      q(500, "The file failed to save.");
                    };
                    x.truncate(w.size);
                  },
                  q
                );
              });
            };
            if (s.createWriter) {
              p(s);
            } else {
              chrome.fileSystem.getWritableEntry(s, p, function (v) {
                q(500, k(v));
              });
            }
          }
        },
        readTextData: function (q, r, p) {
          if (typeof q === "string") {
            GLIFFY.IO.os.getFileEntry(
              q,
              null,
              function (s) {
                GLIFFY.IO.os.readTextData(s, r, p);
              },
              p
            );
          } else {
            q.file(function (t) {
              var s = new FileReader();
              s.onerror = function (u) {
                console.error(u);
                p(500, "The file failed to open.");
              };
              s.onload = function (v) {
                var u = v.target.result;
                r(u);
              };
              s.readAsText(t);
            });
          }
        },
        readJsonData: function (q, r, p) {
          GLIFFY.IO.os.readTextData(
            q,
            function (s) {
              try {
                r(JSON.parse(s));
              } catch (t) {
                p(
                  500,
                  "The file could not be loaded because it is not a recognizable format."
                );
              }
            },
            p
          );
        },
        getDirectory: function (s, p, u, q) {
          var t, r;
          s = GLIFFY.IO.path.normalize(s);
          if (s === "") {
            if (p) {
              u(p);
            } else {
              GLIFFY.IO.os.getFileSystem(function (v) {
                u(v.root);
              }, q);
            }
          }
          t = s.split("/");
          r = function (v) {
            if (t.length === 0) {
              u(v);
            } else {
              v.getDirectory(
                t[0],
                { create: true },
                function (w) {
                  t.shift();
                  r(w);
                },
                m(GLIFFY.IO.os, "getDirectory", arguments, q)
              );
            }
          };
          if (p) {
            r(p);
          } else {
            GLIFFY.IO.os.getFileSystem(function (v) {
              r(v.root);
            }, q);
          }
        },
        getDirectoryListing: function (q, t, s) {
          var u, r, p;
          if (typeof q === "string") {
            GLIFFY.IO.os.getDirectory(
              q,
              null,
              function (v) {
                GLIFFY.IO.os.getDirectoryListing(v, t, s);
              },
              s
            );
          } else {
            u = [];
            r = q.createReader();
            p = function () {
              r.readEntries(
                function (v) {
                  if (!v.length) {
                    t(u);
                  } else {
                    u = u.concat(g(v));
                    p();
                  }
                },
                function (v) {
                  console.error(v);
                  s(500, "The directory could not be read.");
                }
              );
            };
            p();
          }
        },
        getEntry: function (r, p, s, q) {
          GLIFFY.IO.os.getFileEntry(r, p, s, function () {
            GLIFFY.IO.os.getDirectory(r, p, s, q);
          });
        },
        remove: function (p, r, q) {
          if (typeof p === "string") {
            GLIFFY.IO.os.getEntry(
              p,
              null,
              function (s) {
                GLIFFY.IO.os.remove(s, r, q);
              },
              q
            );
          } else {
            if (p.isDirectory) {
              p.removeRecursively(r, function (s) {
                console.error(s);
                q(500, k(s));
              });
            } else {
              p.remove(r, function (s) {
                console.error(s);
                q(500, k(s));
              });
            }
          }
        },
        getModifiedDate: function (p, r, q) {
          if (typeof p === "string") {
            GLIFFY.IO.os.getEntry(
              p,
              null,
              function (s) {
                GLIFFY.IO.os.getModifiedDate(s, r, q);
              },
              q
            );
          } else {
            p.getMetadata(
              function (s) {
                r(s.modificationTime);
              },
              function (s) {
                q(500, k(s));
              }
            );
          }
        },
      },
      openGonFile: function (r, p, q) {
        this.openGonFileHandler(
          "openWritableFile",
          function (s) {
            s.read(r, p);
          },
          q
        );
      },
      promptSaveGonFile: function (r, p, q) {
        this.openGonFileHandler(
          "saveFile",
          function (s) {
            s.getDocumentInfo(r);
          },
          q
        );
      },
      writeGonFile: function (p, s, r) {
        var q = this.getGonFileHandler(p);
        if (q) {
          q.write(p, s, r);
        } else {
          if (p && p.urls) {
            r(404, "No such file exists:" + p.urls.path);
          } else {
            r(404, "No such file exists...");
          }
        }
      },
      readGonFile: function (p, s, r) {
        var q = this.getGonFileHandler(p);
        if (q) {
          q.read(s, r);
        } else {
          if (p && p.urls) {
            r(404, "No such file exists:" + p.urls.path);
          } else {
            r(404, "No such file exists...");
          }
        }
      },
      getGonFileHandler: function (p) {
        if (p && p.urls && p.urls.path in n) {
          return n[p.urls.path];
        } else {
          return null;
        }
      },
      openGonFileHandler: function (q, s, r) {
        var p = h;
        if (q == "saveFile") {
          p = d;
        }
        chrome.fileSystem.chooseEntry({ type: q, accepts: p }, function (t) {
          if (t) {
            s(new j(t));
          } else {
            if (r) {
              r();
            }
          }
        });
      },
      newGonHandler: function (r, p) {
        var q = new j(r);
        q.cache(p);
      },
      writeTempGonFile: function (p, r, q) {
        GLIFFY.IO.getTempGonFileHandler(
          p,
          function (s) {
            s.write(
              p,
              function (t) {
                s.close();
                r();
              },
              q
            );
          },
          q
        );
      },
      openTempGonDocuments: function (q, p) {
        GLIFFY.IO.os.getDirectoryListing(
          c,
          function (x) {
            var t = [],
              s,
              r,
              w = [],
              v,
              u = 0;
            for (s = 0; s < x.length; s++) {
              v = x[s];
              if (v.isFile) {
                w.push(v);
              }
            }
            if (w.length > 0) {
              u = w.length;
              for (s = 0; s < w.length; s++) {
                o(
                  new b(w[s]),
                  function (y) {
                    t.push(y);
                    if (t.length === u) {
                      q(t);
                    }
                  },
                  function (y, z) {
                    u--;
                    console.error(z);
                    if (t.length === u) {
                      q(t);
                    }
                  }
                );
              }
            } else {
              q(t);
            }
          },
          p
        );
      },
      getTempGonFileHandler: function (p, r, q) {
        GLIFFY.IO.os.getFileEntry(
          a(p),
          null,
          function (s) {
            r(new b(s));
          },
          q
        );
      },
      removeTempGonFile: function (p, r, q) {
        if (p.urls.tempId !== undefined && p.urls.tempId !== null) {
          GLIFFY.IO.os.getFileEntry(
            a(p),
            null,
            function (s) {
              s.remove(r, function (t) {
                q(500, "Could not delete the temp document");
              });
            },
            q
          );
        }
      },
    };
    function o(p, r, q) {
      p.read(
        function (s) {
          r(s);
        },
        function (s, t) {
          p.remove(function () {
            q(s, t);
          }, q);
        }
      );
    }
  })();
})();
