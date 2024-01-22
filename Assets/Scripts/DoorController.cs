using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    
    public void StageComplete(){
        animator.SetTrigger("keyComplete");
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name == "Player"){
            Debug.Log("stage complete");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
