document.addEventListener('DOMContentLoaded', function() {
  const toggleBtn      = document.getElementById('toggle-btn');
  const resultTextbox  = document.getElementById('result');
  const languageSelect = document.getElementById('language-select');
  const copyBtn        = document.getElementById('copy-btn');
  const clearBtn       = document.getElementById('clear-btn');
  const statusDiv      = document.getElementById('status');
  const darkModeBtn    = document.getElementById('dark-mode-btn');
  
  /************************************************/
  let isListening = false;
  /************************************************/
  if (localStorage.getItem('savedText')) {
    resultTextbox.value = localStorage.getItem('savedText');
  }
  if (localStorage.getItem("dark-mode") === "enabled") {
    document.body.classList.add("dark-mode");
  }
  /************************************************/
  if (!('webkitSpeechRecognition' in window)) {
    alert('此瀏覽器不支援 Web Speech API。請升級或使用 Chrome。');
    return;
  }
  /************************************************/
  const recognition = new webkitSpeechRecognition();
  {
    recognition.continuous = false;
    recognition.interimResults = false;
    recognition.lang = 'zh-TW';
  }

  recognition.onstart = function() {
    statusDiv.textContent = '狀態: 正在聆聽...';
    toggleBtn.textContent = '🛑 停止聆聽';
    isListening = true;
  };

  recognition.onresult = function(event) {
    const transcript = event.results[0][0].transcript;
    const newText = transcript;

    const start = resultTextbox.selectionStart;
    const end = resultTextbox.selectionEnd;

    resultTextbox.value = resultTextbox.value.slice(0, start) + newText + resultTextbox.value.slice(end);
    // resultTextbox.value += newText;
    resultTextbox.selectionStart = resultTextbox.selectionEnd = start + newText.length;
    resultTextbox.focus();

    // Save text to localStorage
    localStorage.setItem('savedText', resultTextbox.value);
  };
  /************************************************/
  recognition.onerror = function(event) {
    statusDiv.textContent = `狀態: 出錯 (${event.error})`;
    toggleBtn.textContent = '🎤 開始聆聽';
    isListening = false;
  };
  /************************************************/
  recognition.onend = function() {
    statusDiv.textContent = '狀態: 聆聽結束';
    toggleBtn.textContent = '🎤 開始聆聽';
    isListening = false;
    // Save text to localStorage
    localStorage.setItem('savedText', resultTextbox.value);
  };
  /************************************************/
  toggleBtn.onclick = function() {
    if (isListening) {
      recognition.stop();
    } else {
      recognition.lang = languageSelect.value;
      recognition.start();
    }
  };
  /************************************************/
  copyBtn.onclick = function() {
    resultTextbox.select();
    document.execCommand('copy');
    alert('文本已複製到剪貼板');
  };
  /************************************************/
  clearBtn.onclick = function() {
    resultTextbox.value = '';
    localStorage.removeItem('savedText');
    statusDiv.textContent = '狀態: 等待中...';
  };
  /************************************************/
  resultTextbox.oninput = function() {
    localStorage.setItem('savedText', resultTextbox.value);
  };
  /************************************************/
  darkModeBtn.onclick = function() {
    document.body.classList.toggle('dark-mode');
    if (document.body.classList.contains("dark-mode")) {
      localStorage.setItem("dark-mode", "enabled");
      console.log("A");
    } else {
      localStorage.setItem("dark-mode", "disabled");
      console.log("B");
    }
  };
});