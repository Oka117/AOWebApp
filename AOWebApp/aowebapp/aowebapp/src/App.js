import './App.css';
import Card from './components/Card';
import CardV2 from './components/CardV2';
import CardV3 from './components/CardV3';
import CardList from './components/CardList';
function App() {
    return (
        <div className="App container">
            <div className="bg-light py-1 mb-2">
                <h2 className="text-center">Example Application</h2>
            </div>

            <div className="row justify-content-center">
                <Card
                    itemId="1"
                    itemName="test record1"
                    itemDescription="test record 1 Desc"
                    itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"
                    itemCost="15.00"
                />
            </div>
            <div className="row justify-content-center">
                <CardV2
                    itemId="2"
                    itemName="test record2"
                    itemDescription="test record 2 Desc"
                    itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"
                    itemCost="10.00"
                />
            </div>
            <div className="row justify-content-center">
                <CardV3
                    itemId="3"
                    itemName="test record3"
                    itemDescription="test record 3 Desc"
                    itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"
                    itemCost="10.00"
                />
                <CardList />
            </div>
        </div>
    );
}

export default App;
