import React, { useState, useEffect } from "react";
import "./HistoryPage.css";

//TODO: Clean up display, make it look neat, and fiddle with the CSS file a bit.
function HistoryPage() {
	const [calculations, setCalculations] = useState([]);
	const [conversions, setConversions] = useState([]);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
        fetch("http://localhost:7071/api/history/all")
            .then((res) => res.json())
            .then(data => {
		    setCalculations(data.calculations || []);
		    setConversions(data.conversions || []);
		    setLoading(false);
            })
	    .catch(err => {
		    console.error("Failed to fetch history:", err);
		    setLoading(false);
	    });
    	}, []);

    	if (loading) {
		return <h3>Loading...</h3>;
    	}
	return (
		<div className="history-container">
		    {/* Calculations */}
		    <div className="history-column">
			<h4>Calculations</h4>
			{calculations.length === 0 ? (
			    <p>No calculation history.</p>
			) : (
			    <div className="entry-list">
				{calculations.map((item) => (
				    <div key={item.RowKey} className="entry-box">
					<strong>{item.A} {item.Operation} {item.B} = {item.Result}</strong>
					<p>{item.Timestamp}</p>
				    </div>
				))}
			    </div>
			)}
		    </div>
			
		    {/* Conversions */}
		    <div className="history-column">
			<h4>Conversions</h4>
			{conversions.length === 0 ? (
			    <p>No conversion history.</p>
			) : (
			    <div className="entry-list">
				{conversions.map((item) => (
				    <div key={item.RowKey} className="entry-box">
					<strong>{item.A} {item.Operation} {item.B} = {item.Result}</strong>
				    </div>
				))}
			    </div>
			)}
		    </div>
		</div>
		);
};
export default HistoryPage;
