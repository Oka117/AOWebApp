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

    function onSubmit(e) {
        e.preventDefault();

        const form = e.target;
        const formData = new FormData(form);
        console.log("FromData: " + formData.get("searchText"));
        setQuery(formData.get("searchText"))
    }

    //console.log("cardData: ", cardData);
    return (
        <div id="cardListSearch">
            <form method="post" onSubmit={onSubmit} className="row justify-content-start mb-3">
           
                <div className="col-3">
                    <input type="text" name="searchText" className="form-control" placeholder="Type your query" />
                </div>
                <div className="col text-left">
                    <button type="submit" value={searchQuery}>Search</button>
                </div>
              
            </form>
            <div id="cardList" className="row">
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