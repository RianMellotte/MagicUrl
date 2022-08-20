import { Outlet, Route } from "react-router-dom";
import ErrorPage from "./Components/ErrorPage";
import InputPage from "./Components/InputPage";

function App() {
  
return (
    <div className="App">
      <h1>Magic Url Shortener</h1>
      <Outlet />
    </div>
  );
}

export default App;
