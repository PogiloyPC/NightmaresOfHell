
public class ButtonCreaterExtension : GameButton
{    
    private BuilderPanel _builderPanel;

    private ICreaterExtension _creater;

    protected override void OnEnable()
    {
        base.OnEnable();

        _onClick += StartCreateExtension;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _onClick -= StartCreateExtension;
    }

    public void Init(BuilderPanel builderPanel, ICreaterExtension creater)
    {
        _builderPanel = builderPanel;

        _creater = creater;
    }

    private void StartCreateExtension()
    {
        _creater.BuildExtension();

        _builderPanel.UndisplayPanel();
    }
}
