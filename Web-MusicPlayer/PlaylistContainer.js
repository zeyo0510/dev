// PlaylistContainer.js
class PlaylistContainer extends HTMLElement {
  constructor() {
    super();
    this.attachShadow({ mode: 'open' });
    this.playlist = [];
    this.currentTrackIndex = 0;
    this.currentFilter = 'all';
    
    // 綁定方法
    this.renderPlaylist = this.renderPlaylist.bind(this);
    this.updateActiveItem = this.updateActiveItem.bind(this);
  }

  connectedCallback() {
    this.render();
    this.setupEventListeners();
  }

  set playlistData(data) {
    this.playlist = data;
    this.renderPlaylist();
  }

  set currentIndex(index) {
    this.currentTrackIndex = index;
    this.updateActiveItem();
  }

  set filterType(type) {
    this.currentFilter = type;
    this.renderPlaylist();
  }

  render() {
    this.shadowRoot.innerHTML = `
      <style>
        :host {
          display: block;
          max-height: 0;
          overflow: hidden;
          transition: max-height 0.3s ease-out;
          background-color: #f0f0f0;
        }
        
        :host(.show) {
          max-height: 200px;
          overflow-y: auto;
        }
        
        .playlist-filter {
          display: flex;
          justify-content: center;
          padding: 8px 0;
          background-color: #e8e8e8;
          border-bottom: 1px solid #ddd;
        }
        
        .playlist-filter button {
          background: none;
          border: none;
          cursor: pointer;
          font-size: 12px;
          color: #555;
          padding: 4px 8px;
          margin: 0 4px;
          border-radius: 3px;
        }
        
        .playlist-filter button.active {
          background-color: #d0d0d0;
          font-weight: bold;
        }
        
        .playlist-item {
          display: flex;
          justify-content: space-between;
          align-items: center;
          padding: 10px 15px;
          border-bottom: 1px solid #ddd;
          cursor: pointer;
          font-size: 14px;
          white-space: nowrap;
          overflow: hidden;
          text-overflow: ellipsis;
        }
        
        .playlist-item:hover {
          background-color: #e0e0e0;
        }
        
        .playlist-item.active {
          background-color: #d0d0d0;
          font-weight: bold;
        }
        
        .playlist-item-content {
          flex: 1;
          overflow: hidden;
          text-overflow: ellipsis;
        }
        
        .playlist-item-actions {
          display: none;
          margin-left: 10px;
        }
        
        .playlist-item:hover .playlist-item-actions {
          display: flex;
        }
        
        .playlist-item-action {
          background: none;
          border: none;
          color: #666;
          cursor: pointer;
          font-size: 18px;
          margin-left: 5px;
          padding: 2px;
          display: flex;
          align-items: center;
          justify-content: center;
        }
        
        .playlist-item-action:hover {
          color: #333;
        }
        
        .playlist-controls {
          display: flex;
          justify-content: space-between;
          padding: 8px 15px;
          background-color: #e0e0e0;
          border-top: 1px solid #ddd;
        }
        
        .playlist-controls button {
          background: none;
          border: none;
          cursor: pointer;
          display: flex;
          align-items: center;
          justify-content: center;
          padding: 6px;
        }
        
        .playlist-controls button .material-icons {
          font-size: 18px;
        }
        
        .playlist-controls button:hover {
          color: #000;
          background-color: #d0d0d0;
          border-radius: 3px;
        }
        
        .material-icons {
          font-family: 'Material Icons';
          font-weight: normal;
          font-style: normal;
          font-size: 24px;
          line-height: 1;
          letter-spacing: normal;
          text-transform: none;
          display: inline-block;
          white-space: nowrap;
          word-wrap: normal;
          direction: ltr;
          -webkit-font-feature-settings: 'liga';
          -webkit-font-smoothing: antialiased;
        }
        
        .favorite-icon {
          margin-right: 8px;
          color: #ff4081;
          cursor: pointer;
        }
        
        .favorite-icon.favorited {
          color: #ff4081;
        }
      </style>
      <div class="playlist-filter">
        <button id="filter-all" class="active">全部</button>
        <button id="filter-favorites">我的最愛</button>
      </div>
      <div class="playlist-items-container"></div>
      <div class="playlist-controls">
        <button id="clear-playlist" title="清除播放清單">
          <span class="material-icons">delete_sweep</span>
        </button>
        <button id="add-files" title="加入更多檔案">
          <span class="material-icons">add</span>
        </button>
      </div>
    `;
  }

  setupEventListeners() {
    this.shadowRoot.getElementById('filter-all').addEventListener('click', () => {
      this.currentFilter = 'all';
      this.shadowRoot.getElementById('filter-all').classList.add('active');
      this.shadowRoot.getElementById('filter-favorites').classList.remove('active');
      this.renderPlaylist();
      this.dispatchEvent(new CustomEvent('filter-change', { detail: 'all' }));
    });
    
    this.shadowRoot.getElementById('filter-favorites').addEventListener('click', () => {
      this.currentFilter = 'favorites';
      this.shadowRoot.getElementById('filter-favorites').classList.add('active');
      this.shadowRoot.getElementById('filter-all').classList.remove('active');
      this.renderPlaylist();
      this.dispatchEvent(new CustomEvent('filter-change', { detail: 'favorites' }));
    });
    
    this.shadowRoot.getElementById('clear-playlist').addEventListener('click', () => {
      this.dispatchEvent(new Event('clear-playlist'));
    });
    
    this.shadowRoot.getElementById('add-files').addEventListener('click', () => {
      this.dispatchEvent(new Event('add-files'));
    });
  }

  renderPlaylist() {
    const itemsContainer = this.shadowRoot.querySelector('.playlist-items-container');
    itemsContainer.innerHTML = '';
    
    // 根據當前過濾條件顯示歌曲
    const filteredPlaylist = this.currentFilter === 'favorites' 
      ? this.playlist.filter(track => track.isFavorite) 
      : this.playlist;
    
    if (filteredPlaylist.length === 0) {
      const emptyMsg = document.createElement('div');
      emptyMsg.className = 'playlist-item';
      emptyMsg.textContent = this.currentFilter === 'favorites' ? '沒有最愛歌曲' : '播放清單為空';
      itemsContainer.appendChild(emptyMsg);
      return;
    }
    
    filteredPlaylist.forEach((track, index) => {
      const originalIndex = this.playlist.findIndex(t => t.audio === track.audio);
      const playlistItem = document.createElement('div');
      playlistItem.className = 'playlist-item';
      playlistItem.innerHTML = `
        <span class="material-icons favorite-icon ${track.isFavorite ? 'favorited' : ''}" 
              data-index="${originalIndex}">
          ${track.isFavorite ? 'favorite' : 'favorite_border'}
        </span>
        <div class="playlist-item-content">${track.title}${track.artist ? ' - ' + track.artist : ''}</div>
        <div class="playlist-item-actions">
          <button class="playlist-item-action" title="上移" data-index="${originalIndex}" data-action="move-up">
            <span class="material-icons">arrow_upward</span>
          </button>
          <button class="playlist-item-action" title="下移" data-index="${originalIndex}" data-action="move-down">
            <span class="material-icons">arrow_downward</span>
          </button>
          <button class="playlist-item-action" title="移除" data-index="${originalIndex}" data-action="remove">
            <span class="material-icons">close</span>
          </button>
        </div>
      `;
      
      playlistItem.addEventListener('click', (e) => {
        if (!e.target.closest('.playlist-item-action') && 
            !e.target.classList.contains('material-icons') &&
            !e.target.classList.contains('favorite-icon')) {
          this.dispatchEvent(new CustomEvent('track-select', { 
            detail: originalIndex 
          }));
        }
      });
      
      itemsContainer.appendChild(playlistItem);
    });
    
    // 添加事件監聽器
    this.shadowRoot.querySelectorAll('.favorite-icon').forEach(icon => {
      icon.addEventListener('click', (e) => {
        e.stopPropagation();
        const index = parseInt(e.target.getAttribute('data-index'));
        this.dispatchEvent(new CustomEvent('toggle-favorite', { 
          detail: index 
        }));
      });
    });
    
    this.shadowRoot.querySelectorAll('[data-action="move-up"]').forEach(btn => {
      btn.addEventListener('click', (e) => {
        e.stopPropagation();
        const index = parseInt(e.target.closest('button').getAttribute('data-index'));
        this.dispatchEvent(new CustomEvent('move-track-up', { 
          detail: index 
        }));
      });
    });
    
    this.shadowRoot.querySelectorAll('[data-action="move-down"]').forEach(btn => {
      btn.addEventListener('click', (e) => {
        e.stopPropagation();
        const index = parseInt(e.target.closest('button').getAttribute('data-index'));
        this.dispatchEvent(new CustomEvent('move-track-down', { 
          detail: index 
        }));
      });
    });
    
    this.shadowRoot.querySelectorAll('[data-action="remove"]').forEach(btn => {
      btn.addEventListener('click', (e) => {
        e.stopPropagation();
        const index = parseInt(e.target.closest('button').getAttribute('data-index'));
        this.dispatchEvent(new CustomEvent('remove-track', { 
          detail: index 
        }));
      });
    });
    
    this.updateActiveItem();
  }

  updateActiveItem() {
    this.shadowRoot.querySelectorAll('.playlist-item').forEach((item, i) => {
      const originalIndex = this.playlist.findIndex(t => t.audio === (this.currentFilter === 'favorites' 
        ? this.playlist.filter(t => t.isFavorite)[i]?.audio 
        : this.playlist[i]?.audio));
      item.classList.toggle('active', originalIndex === this.currentTrackIndex && !item.textContent.includes('播放清單為空'));
    });
  }

  toggle() {
    this.classList.toggle('show');
  }
}

customElements.define('playlist-container', PlaylistContainer);