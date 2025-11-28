import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";

const Calculator = () => <h2>Calculator</h2>;
const Converter = () => <h2>Unit Conversion</h2>;
const History = () => <h2>History</h2>;

function App() {
  return (
	<Router>
		<div style={{ padding: "20px" }}>
			<h1>AzureCalc</h1>
	  		<nav style={{ marginBottom: "20px" }}>
	  			<Link to="/calculator" style={{ marginRight: "15px" }}>Calculator</Link>
	  			<Link to="/converter" style={{ marginRight: "15px" }}>Converter</Link>
	  			<Link to="/history">History</Link>
	  		</nav>
	
		  	<Routes>
		  		<Route path="/calculator" element={<Calculator />} />
		  		<Route path="/converter" element={<Converter />} />
		  		<Route path="/history" element={<History />} />
		  		<Route path="/" element={<h2>Welcome to AzureCalc!</h2>} />
		  	</Routes>
		</div>
	</Router>
  );
}

export default App;

