# CMS2
clone repository
go to tools -> package manager -> restore packages

# To use the test cases
### install the dependency 
`pip install -r requirements.txt`

### open a new terminal and run the load server
`locust --host=http://localhost:59460`

### open a new terminal and run the graph server
`bokeh serve`


### lastly another terminal and plot the graph
`python plotter.py`



