create or alter procedure [wallet].[wallet_balance_get]
(
	@account int
)
as
begin
	if not exists (select 1 from [wallet].[account] where id = @account)
	begin
		RAISERROR('Account is not valid!', 16, 1);
		return;
	end
	else
	begin
		declare @balance int = 0
		
		select @balance = sum(
			case when direction = 0
				then amount
			when direction = 1
				then -amount
			end) 
		from [wallet].[transaction]
		where accountId = @account

		return @balance
	end
end