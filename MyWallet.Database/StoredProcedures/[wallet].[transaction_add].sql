create or alter procedure [wallet].[transaction_add]
(
	@id int,
	@amount decimal,
	@direction bit,
	@account int
)
as
begin
	begin
		if not exists(select 1 from [wallet].[Account] where id = @account)
		begin
			RAISERROR('Account does not exist!', 16, 1);
			return
		end
	end

	if (@direction = 1)
	begin
		declare @balance decimal
		exec @balance = [wallet].[wallet_balance_get] @account
		if (@balance < @amount)
		begin
			RAISERROR('Account does not have sufficient balance for this transaction!', 16, 1);
			return;
		end
	end

	insert into [wallet].[transaction]
		(referenceId, amount, direction, accountId)
	values
		(@id, @amount, @direction, @account)

	return SCOPE_IDENTITY()
end