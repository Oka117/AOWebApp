import React, { useState, useEffect } from 'react'
import Card from "./CardV3"
//import cardData from "../assets/itemData.json"


const CardListSearch = ({ }) => {
    const [cardData, setState] = useState([]);
    const [query, setQuery] = useState('');

    React.useEffect(() => {
        console.log("useEffect");
        fetch(`http://localhost:5180/api/itemsWebAPI?searchText=${query}`)
            .then(response => response.json())
            .then(data => setState(data))
            .catch(err => {
                console.log(err);
            });
    }, [query])

    function searchQuery(evt) {
        const value = document.querySelector('[name="searchText"]').value;
        alert('value: ' + value);
        setQuery(value);
    }

    //console.log("cardData: ", cardData);
    return (
        <div id="cardListSearch">
          <div className="row justify-content-start mb-3">
            <div className="col-3">
                <input type="text" name="searchText" className="form-control" placeholder="Type your query" />
            </div>
            <div className="col text-left">
                <button type="button" className="btn btn-primary" onClick={searchQuery}>Search</button>
            </div>
        </div>
        <div id = "cardList" className="row">
            {cardData.map((obj) => (
                <Card
                    key={obj.itemId}
                    itemId={obj.itemId}
                    itemName={obj.itemName}
                    itemDescription={obj.itemDescription}
                    itemCost={obj.itemCost}
                    itemImage={obj.itemImage}
                />
            ))}
            </div>
        </div>
    )
}

export default CardListSearch