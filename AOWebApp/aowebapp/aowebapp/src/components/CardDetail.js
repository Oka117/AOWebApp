import { useState, useEffect } from "react"
import { Link, useParams } from "react-router-dom";

const CardDetail = ({ }) => {
    let params = useParams();
    const [cardData, setState] = useState({});
    const [itemId, setItemId] = useState(params.itemId);

    useEffect(() => {
        console.log("useEffect");
        fetch(`http://localhost:5180/api/itemsWebAPI/${itemId}`)
            .then(response => response.json())
            .then(data => setState(data))
            .catch(err => {
                console.log(err);
            });
    }, [itemId])

    return (
        <div className="card mb-2" style={{ width: 18 + 'rem' }}>
            <img className="card-img-top" src={cardData.itemImage} alt={"Image of " + cardData.itemName} />
            <div className="card-body">
                <h5 className="card-title">{cardData.itemName}</h5>
                <p className="card-text">{cardData.itemDescription}</p>
                <p className="card-text">{cardData.itemCost}</p>
                <Link to="/Products" className="btn btn-primary">Back To Products</Link>

            </div>
        </div>

    )

}

export default CardDetail