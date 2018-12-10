import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Question } from '../home/question.model';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-admin-component',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})

export class AdminComponent {
  selectedQuestion: Question;
  allQuestions: Question[];
  allNotAnswered: Question[];
  myappurl: string;
  showForm: boolean;
  showEditForm: boolean;
  showQuestionsList: boolean;
  showNotAnsweredQuestions: boolean;
  formStatus: string;
  angularForm = new FormGroup({
    question: new FormControl(),
    answer: new FormControl(),
    categoryTitle: new FormControl(),
  });
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private fb: FormBuilder) {
    this.myappurl = baseUrl;
    this.createForm();
  }

  createForm() {
    this.angularForm = this.fb.group({
      id: [''],
      question: ['', Validators.required],
      answer: ['', Validators.required],
      categoryTitle: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.getNotAnsweredQuestions();
    this.getQuestions();
    this.showForm = false;
    this.showQuestionsList = true;
    this.showNotAnsweredQuestions = true;
  }

  getQuestions() {
    this.http.get<Question[]>(this.myappurl + 'api/MyApp').subscribe(result => {
      this.allQuestions = result;
    }, error => console.error(error));
  };

  getNotAnsweredQuestions() {
    this.http.get<Question[]>(this.myappurl + 'api/Customer').subscribe(result => {
      this.allNotAnswered = result;
    }, error => console.error(error));
  }

  addNewQuestion() {
    this.showQuestionsList = false;
    this.showNotAnsweredQuestions = false;
    this.showForm = true;
    this.formStatus = "Register";
  }

  edit(id: number) {
    this.http.get<Question>("api/MyApp/" + id)
      .subscribe(
        result => {
          this.selectedQuestion = result;
          this.angularForm.patchValue({ id: result.id });
          this.angularForm.patchValue({ question: result.question_ });
          this.angularForm.patchValue({ answer: result.answer });
          this.angularForm.patchValue({ categoryTitle: result.categoryTitle });
        },
        error => alert(error),
        () => console.log("done get-api/question")
      );
    this.formStatus = "Edit";
    this.showForm = true;
    this.showQuestionsList = false;
    this.showNotAnsweredQuestions = false;
  }

  onSubmit() {
    if (this.formStatus == "Register") {
      this.addQuestion();
    }
    else if (this.formStatus == "Edit") {
      this.editQuestion();
    }
    else {
      alert("Some mistake occured!");
    }
  }

  backToList() {
    this.showQuestionsList = true;
    this.showForm = false;
  }

  addQuestion() {
    var question = new Question();
    question.question_ = this.angularForm.value.question;
    question.answer = this.angularForm.value.answer;
    question.categoryTitle = this.angularForm.value.categoryTitle;
    var body: string = JSON.stringify(question);
    var headers = new HttpHeaders({ "Content-Type": "application/json" });

    this.http.post("api/MyApp", body, { headers: headers })
      .subscribe(
        result => {
          this.getQuestions();
          this.showForm = false;
          this.showQuestionsList = true;
        },
        error => alert(error),
        () => console.log("done post-api/question")
      );
  }

  editQuestion() {
    var question = new Question();

    question.question_ = this.angularForm.value.question;
    question.answer = this.angularForm.value.answer;
    question.categoryTitle = this.angularForm.value.categoryTitle;

    var body: string = JSON.stringify(question);
    var headers = new HttpHeaders({ "Content-Type": "application/json" });

    this.http.put("api/MyApp/" + this.angularForm.value.id, body, { headers: headers })
      .subscribe(
        result => {
          this.getQuestions();
          this.getNotAnsweredQuestions();
          this.showForm = false;
          this.showQuestionsList = true;
          this.showNotAnsweredQuestions = true;
        },
        error => alert(error),
        () => console.log("done post-api/question")
      );
  }

  delete(id: number) {
    this.http.delete("api/MyApp/" + id)
      .subscribe(
        result => {
          this.getQuestions();
          this.getNotAnsweredQuestions();
        },
        error => alert(error),
        () => console.log("done delete-api/question")
      );
  }
}

