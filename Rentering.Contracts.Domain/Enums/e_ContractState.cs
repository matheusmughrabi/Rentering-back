namespace Rentering.Contracts.Domain.Enums
{
    public enum e_ContractState
    {
        // Sem 2 ou mais participantes, esperando participantes aceitarem, criado, expirado
        NotEnoughParticipants = 1,
        WaitingParticipantsAccept = 2,
        Active = 3,
        Expired = 4
    }
}
