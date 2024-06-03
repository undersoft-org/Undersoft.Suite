namespace Undersoft.SDK.Service.Application.GUI.Models;

public class StateFlags
{
    public bool Canceled { get; set; }

    public bool Checked { get; set; }

    public bool Selected { get; set; }

    public bool Editing { get; set; }

    public bool Edited { get; set; }

    public bool Updated { get; set; }

    public bool Changed { get; set; }

    public bool Added { get; set; }

    public bool Deleted { get; set; }

    public bool Expanded { get; set; }

    public bool Hidden { get; set; }

    public bool Disabled { get; set; }

    public bool HaveNext { get; set; }

    public void ClearCommandStates()
    {
        Editing = false;
        Edited = false;
        Updated = false;
        Changed = false;
        Added = false;
        Deleted = false;
        Canceled = false;
    }

    public void ClearMarkingStates()
    {
        Checked = false;
        Selected = false;
        Disabled = false;
        Hidden = false;
    }

    public void ClearStates()
    {
        ClearCommandStates();
        ClearMarkingStates();
    }
}
