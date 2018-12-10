import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Question } from './question.model';
import { Category } from './category.model';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  allCategories: Category[];
  showQuestionForm: boolean;
  questionForm = new FormGroup({
    question: new FormControl(),
    customerName: new FormControl(),
    customerSurname: new FormControl(),
    customerEmail: new FormControl(),
  });
  showChildren: boolean;
  vote = 1;

  show(node) {
    node.showChildren = !node.showChildren;
  }

  constructor(private http: HttpClient, private fb: FormBuilder) {
    this.createForm();
  }

  ngOnInit() {
    this.showQuestionForm = false;
    this.getCategories();
  }

  createForm() {
    this.questionForm = this.fb.group({
      id: [''],
      question: [null, Validators.compose([Validators.required, Validators.pattern("^\\W*(?:\\w+\\b\\W*){3,50}$")])],
      customerName: [null, Validators.compose([Validators.required, Validators.pattern("^[a-zA-Z '.-]*$")])],
      customerSurname: [null, Validators.compose([Validators.required, Validators.pattern("^[A-Za-zÀ-ÖØ-öø-ÿ '\-\.]{2,22}$")])],
      customerEmail: [null, Validators.compose([Validators.required, Validators.pattern("^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")])]
    });
  }

  addQuestion() {
    var question = new Question();
    question.question_ = this.questionForm.value.question;
    question.customerName = this.questionForm.value.customerName;
    question.customerSurname = this.questionForm.value.customerSurname;
    question.customerEmail = this.questionForm.value.customerEmail;
    var body: string = JSON.stringify(question);
    var headers = new HttpHeaders({ "Content-Type": "application/json" });

    this.http.post("api/Home", body, { headers: headers })
      .subscribe(
        result => {
          this.showQuestionForm = false;
        },
        error => alert(error),
        () => console.log("done post-api/question")
      );
  }

  onSubmit() {
    this.addQuestion();
  }
  
  getCategories() {
    this.http.get<Category[]>("api/Home")
      .subscribe(
      result => {
        this.allCategories = result;
        },
        error => alert(error),
        () => console.log("done get-api/question")
      );
  }

  showForm() {
    this.showQuestionForm = !this.showQuestionForm;
  }

  thumbup(id: number) {
    this.votingup(id);
  }

  thumbdown(id: number) {
    this.votingdown(id);
  }

  votingup(id: number) {
    var question = new Question();

    question.thumbup = this.vote;

    var body: string = JSON.stringify(question);
    var headers = new HttpHeaders({ "Content-Type": "application/json" });

    this.http.put("api/Home/" + id, body, { headers: headers })
      .subscribe(
      result => {
        this.getCategories();
        },
        error => alert(error),
        () => console.log("done post-api/vote")
      );
  }

  votingdown(id: number) {
    var question = new Question();

    question.thumbdown = this.vote;

    var body: string = JSON.stringify(question);
    var headers = new HttpHeaders({ "Content-Type": "application/json" });

    this.http.put("api/Home/" + id, body, { headers: headers })
      .subscribe(
        result => {
          this.getCategories();
        },
        error => alert(error),
        () => console.log("done post-api/vote")
      );
  }
}
