export interface ICard{
    cardId : number;
    name : string;
    description : string;
    priority : string;
    listId : number;
    createdAt : string;
}

export interface CardModel{
    cards : ICard[],
    card : ICard,
    errorMassege : string 
}