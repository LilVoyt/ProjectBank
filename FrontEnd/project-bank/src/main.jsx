import { render } from 'preact';
import { BrowserRouter } from 'react-router-dom';
import { App } from './app.jsx'
import './index.css'

render(
    <BrowserRouter>
      <App />
    </BrowserRouter>,
    document.getElementById('app')
  );
