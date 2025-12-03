import React, { useEffect, useState } from 'react';

const API_URL = process.env.REACT_APP_API_URL;
// TODO: Restyle this
function UnitConversion() {
	const [categories, setCategories] = useState({});
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState(null);

	useEffect(() => {
		async function fetchCategories() {
			try {
				const res = await fetch(`${API_URL}/api/converter/categories`);
				const data = await res.json();
				setCategories(data);
			} catch (err) {
				setError("Failed to contact API.");
			} finally {
				setLoading(false);
			}
		}

		fetchCategories();
	}, []);

	if (loading) return <h2>Loading...</h2>;

	return (
		<div className="container" style={{ display: "flex", flexDirection: "column", gap: "40px" }}>
		<h1>Unit Conversion</h1>

		{Object.entries(categories).map(([category, units]) => (
			<UnitSection key={category} category={category} units={units} />
		))}
		</div>
	);
}

function UnitSection({ category, units }) {
	const [from, setFrom] = useState(units[0]);
	const [to, setTo] = useState(units[0]);
	const [value, setValue] = useState("");
	const [result, setResult] = useState(null);
	const [error, setError] = useState(null);
	const [loading, setLoading] = useState(false);

	const handleConvert = async (e) => {
		e.preventDefault();

		const num = parseFloat(value);
		if (isNaN(num)) {
			setError("Please enter a valid number.");
			setResult(null);
			return;
		}

		setError(null);
		setResult(null);
		setLoading(true);

		try {
			const res = await fetch(`${API_URL}/api/converter/convert?value=${num}&from=${from}&to=${to}&category=${category}`);

			if (!res.ok) throw new Error("API error");

			const data = await res.json();
			setResult(`${data.value} ${data.from} = ${data.result} ${data.to}`);
		} catch (err) {
			console.error(err);
			setError("Failed to contact API.");
		} finally {
			setLoading(false);
		}
	};

	return (
		<div style={{ padding: "20px", border: "1px solid #ccc", borderRadius: "8px" }}>
		<h2>{category}</h2>

		<form onSubmit={handleConvert} style={{ display: "flex", gap: "10px", flexWrap: "wrap" }}>
		<select value={from} onChange={(e) => setFrom(e.target.value)}>
		{units.map((u) => (
			<option key={u} value={u}>{u}</option>
		))}
		</select>

		<input type="number" value={value} onChange={(e) => setValue(e.target.value)} placeholder="Value" step="any"/>

		<select value={to} onChange={(e) => setTo(e.target.value)}>
		{units.map((u) => (
			<option key={u} value={u}>{u}</option>
		))}
		</select>

		<button type="submit">Convert</button>
		</form>

		{loading && <p>Converting...</p>}
		{error && <p style={{ color: "red" }}>{error}</p>}
		{result && <h3>{result}</h3>}
		</div>

	);

}

export default UnitConversion;

