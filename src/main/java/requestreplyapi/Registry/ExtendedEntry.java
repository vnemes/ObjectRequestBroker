package requestreplyapi.Registry;

public class ExtendedEntry extends Entry{
    private String entryType;

    public ExtendedEntry(String theDest, int thePort, String entryType) {
        super(theDest, thePort);
        this.entryType = entryType;
    }

    public String type(){
        return entryType;
    }

    @Override
    public String toString() {
        return "ExtendedEntry{" +
                "entryType='" + entryType + '\'' +
                super.toString() +
                '}';
    }
}
