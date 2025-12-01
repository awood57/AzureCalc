import React, { useState } from 'react';

const API_URL = process.env.REACT_APP_API_URL;
//TODO: DRY this, will require changing API so that Basic Math is POST
function Calculator() {
	// Basic calculator states
	const [num1, setNum1] = useState("");
	const [num2, setNum2] = useState("");
	const [operation, setOperation] = useState("add");
	const [basicResult, setBasicResult] = useState(null);
  	const [basicLoading, setBasicLoading] = useState(false);
  	const [basicError, setBasicError] = useState(null);

	// TODO: Power calculator states
	const [baseNum, setBaseNum] = useState("");
	const [exp, setExp] = useState("");
	const [powOp, setPowOp] = useState("power");
	const [powerResult, setPowerResult] = useState(null);
	const [powerLoading, setPowerLoading] = useState(false);
	const [powerError, setPowerError] = useState(null);
	

	// Form submission
	const handleBasicSubmit = async (e) => {
		e.preventDefault();
		setBasicLoading(true);
		setBasicError(null);
		setBasicResult(null);

		try {
			const res = await fetch(`${API_URL}/api/calculator/basic?num1=${num1}&num2=${num2}&operation=${operation}`);
			if (!res.ok) throw new Error("API Error");

			const data = await res.json();
			setBasicResult(data.result);
		} catch (err) {
			setBasicError("Failed to contact API.");
		} finally {
			setBasicLoading(false);
		}
	};

	const handlePowerSubmit = async (e) => {
		e.preventDefault();
		setPowerLoading(true);
		setPowerError(null);
		setPowerResult(null);

		try{
			const res = await fetch(`${API_URL}/api/calculator/power`, {
				method: 'POST',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify({
					BaseNum: parseFloat(baseNum),
					ExponentLog: parseFloat(exp),
					Operation: powOp
				})
			});
			if (!res.ok) throw new Error("API Error");

			const data = await res.json();
			setPowerResult(data.result);
		} catch (err) {
			setPowerError("Failed to contact API.");
		} finally {
			setPowerLoading(false);
		}
	};


	return (
		<div className="container" style={{ display: "flex", gap: "40px" }}>
			{/* Basic Math Div */}
			<div style={{ flex: 1 }}>
				<h2>Basic Math</h2>
				<form onSubmit={handleBasicSubmit}>
					<input type="number" value={num1} onChange={(e) => setNum1(e.target.value)} placeholder="First number" />
					<select value={operation} onChange={(e) => setOperation(e.target.value)}>
						<option value="add">+</option>
            					<option value="sub">-</option>
            					<option value="mul">*</option>
            					<option value="div">/</option>
					</select>
					
					<input type="number" value={num2} onChange={(e) => setNum2(e.target.value)} placeholder="Second number" />

					<button type="submit">Calculate</button>
				</form>

				{/* Result display */}
				{basicLoading && <p>Calculating...</p>}
				{basicError && <p style={{ color: "red" }}>{basicError}</p>}
				{basicResult !== null && <h3>Result: {basicResult}</h3>}
			</div>
			{/* Powers Div */}
			<div style={{ flex: 1 }}>
				<h2>Powers & Logarithms</h2>
				<form onSubmit={handlePowerSubmit}>
					<input type="number" value={baseNum} onChange={(e) => setBaseNum(e.target.value)} placeholder="Base number" />
					<select value={powOp} onChange={(e) => setPowOp(e.target.value)}>
						<option value="power">x^y</option>
            					<option value="log">logx(y)</option>
					</select>
					
					<input type="number" value={exp} onChange={(e) => setExp(e.target.value)} placeholder="Second number" />

					<button type="submit">Calculate</button>
				</form>

				{/* Result display */}
				{powerLoading && <p>Calculating...</p>}
				{powerError && <p style={{ color: "red" }}>{powerError}</p>}
				{powerResult !== null && <h3>Result: {powerResult}</h3>}
			</div>
		</div>
	);
}

export default Calculator;
