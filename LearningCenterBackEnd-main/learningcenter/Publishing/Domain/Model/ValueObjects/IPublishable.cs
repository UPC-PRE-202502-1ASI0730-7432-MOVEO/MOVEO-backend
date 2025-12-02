namespace learningcenter.Publishing.Domain.Model.ValueObjects;

public interface IPublishable
{
    void SendToEdit();
    
    void SendToApproval();
    
    void ApprovedAndLock();

    void Reject();

    void ReturnToEdit();
    
}