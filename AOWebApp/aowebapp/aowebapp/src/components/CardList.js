import Card from "./CardV3"
import cardData from "../assets/itemData.json"

const CardList = ({ }) => {
    console.log("cardData: " + cardData);
    return (
        <div className="row">
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
    )
}

export default CardList