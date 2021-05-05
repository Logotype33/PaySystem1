go 
create procedure ReduceFromScore
@idOfScore int, 
@orderItemId int,
@userId int
as
declare @scoreBalance real=(select balance_sum from UnitOfScore where id=@idOfScore) 
declare @orderPrice real=(select price from OrderItem where id=@orderItemId)
declare @sumOfPays real=(select sum_of_pay from Pays where User_id=@userId)
update Pays set Sum_of_pay+=@scoreBalance
update UnitOfScore set @scoreBalance-=@scoreBalance
if @sumOfPays<@orderPrice
return 0
if @sumOfPays>@orderPrice
update UnitOfScore set @scoreBalance=@scoreBalance+(@sumOfPays-@orderPrice)
if @sumOfPays=@orderPrice
return 1
