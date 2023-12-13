using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class CollisionEventBehaviour: MonoBehaviour {

    public CollisionEventBehaviour(string searchedTag = "Untagged") {
        SearchedTag = searchedTag;
    }
    
    public string SearchedTag { get; set; }

    public UnityEvent<Collider> OnMyTriggerEnter { get; } = new UnityEvent<Collider>();
    public UnityEvent<Collider> OnMyTriggerStay { get; } = new UnityEvent<Collider>();
    public UnityEvent<Collider> OnMyTriggerExit { get; } = new UnityEvent<Collider>();

    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals(SearchedTag)) {
            OnMyTriggerEnter.Invoke(other);
        }
    }
    void OnTriggerStay(Collider other) {
        if (other.tag.Equals(SearchedTag)) {
            OnMyTriggerStay.Invoke(other);
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.tag.Equals(SearchedTag)) {
            OnMyTriggerExit.Invoke(other);
        }
    }
}
