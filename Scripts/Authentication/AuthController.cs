using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using System.Collections;

public class AuthController : MonoBehaviour
{
    [SerializeField] Text email, password, message;
    [SerializeField] FirebaseAuth auth;
    [SerializeField] SaveLoadManager slManager;
    [SerializeField] Image screan;
    bool userAuthenticated;
    private void Awake()
    {
        userAuthenticated=FirebaseAuth.DefaultInstance.CurrentUser != null ? true : false;
    }

    
    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        slManager = FindObjectOfType<SaveLoadManager>();
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        if (userAuthenticated)
        {
            screan.gameObject.SetActive(true);
            message.text = "Logging in as " + FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            yield return new WaitForSeconds(1f);
            message.text = "Fetching data ";
        }
        else
        {
            screan.gameObject.SetActive(false);
            message.text = "Please Log in";
        }
    }

    public void Login()
    {
        if (email.text == "" || password.text == "")
        {
            message.text = "Pease enter a valid Email and Password !!";
            return;
        }
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(
           task =>
           {
               if (task.IsCanceled)
               {
                   message.text = "LogIn cancled";
                   return;
               }
               if (task.IsFaulted)
               {
                   message.text = task.Exception.Flatten().InnerExceptions[0].Message;
                   return;
               }
               if (task.IsCompleted)
               {
                   FirebaseUser user = task.Result;
                   message.text = "Logged in as "+ user.Email;
               }
           });
    }


    public void Register()
    {
        if(email.text=="" || password.text=="")
        {
            message.text = "Pease enter a valid Email and Password !!";
            return;
        }
        auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(
            task =>
            {
                if (task.IsCanceled)
                {
                    message.text = "Registeration cancled";
                    return;
                }
                if (task.IsFaulted)
                {
                    print(task.Exception);
                    message.text = task.Exception.Flatten().InnerExceptions[0].Message;
                    return;
                }
                if (task.IsCompleted)
                {
                    FirebaseUser newUser = task.Result;
                    slManager.SaveData(newUser.UserId,newUser.Email);
                    message.text = "User Created Successfully\nEmail: " + newUser.Email + "\nUserId: " + newUser.UserId;                   
                    return;
                }
            }
            );
    }
    public void AnonymousLogin()
    {
        int randomNo = Random.Range(0, 99999);
        string guestId = "Guest" + randomNo.ToString() + "@Project0.com";
        auth.SignInAnonymouslyAsync().ContinueWith(
            task =>
            {
                if (task.IsCanceled)
                {
                    message.text = "LogIn cancled";
                    return;
                }
                if (task.IsFaulted)
                {
                    message.text = task.Exception.Flatten().InnerExceptions[0].Message;
                    return;
                }
                if (task.IsCompleted)
                {
                    FirebaseUser anonymousUser = task.Result;
                    slManager.SaveData(anonymousUser.UserId, guestId);
                    message.text = "User Logged in anonymously\nGuestID: " + guestId+"\nUserId: " + anonymousUser.UserId;
                    return;
                }
            });
    }
    public void Logout()
    { if(auth.CurrentUser!=null)
        {
            message.text = "User " + auth.CurrentUser.UserId + " Logged out";
            auth.SignOut();
        }
        StartCoroutine(Quit());
    }
    private void Update()
    {
        message.SetAllDirty(); 
    }
    IEnumerator Quit()
    {
        message.text = "Exiting App";
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
