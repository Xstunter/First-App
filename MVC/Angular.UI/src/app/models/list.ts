export interface IList{
    listId : number,
    statusName : string,
    boardId : number
}

export interface ListModel{
    lists : IList[],
    list : IList,
    errorMassege : string 
}