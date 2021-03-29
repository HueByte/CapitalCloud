import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router } from 'react-router-dom';
import { Routes } from './routes/Routes';

function App() {
  return (
    <Router>
      <Routes />
    </Router>
  );
}

export default App;
